using GestaoFranquias.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GestaoFranquias.Api.Controllers
{
    [Authorize] // Protege todas as rotas exigindo autenticação JWT
    [ApiController]
    [Route("api/[controller]")]
    public class EstoquesController : ControllerBase
    {
        private readonly IEstoqueRepository _repository;

        public EstoquesController(IEstoqueRepository repository)
        {
            _repository = repository;
        }

        // POST: api/estoques/movimentar
        [HttpPost("movimentar")]
        [Authorize(Roles = "Administrador,Gestor")] // Apenas administradores e gestores da unidade movimentam o estoque manualmente
        public async Task<ActionResult> MovimentarEstoque(int unidadeId, int produtoId, int quantidade)
        {
            try
            {
                // Tenta movimentar o estoque (positivo entra, negativo sai)
                await _repository.MovimentarEstoqueAsync(unidadeId, produtoId, quantidade);
                return Ok(new { mensagem = "Estoque atualizado com sucesso!" });
            }
            catch (InvalidOperationException ex)
            {
                // Regra de negócio: barra saldo negativo e retorna 400 Bad Request
                return BadRequest(new { erro = ex.Message });
            }
        }
        
        // GET: api/estoques/baixo/{unidadeId}
        [HttpGet("baixo/{unidadeId}")]
        [Authorize(Roles = "Administrador,Gestor")] // Gestores e franqueadora acompanham alertas de estoque baixo
        public async Task<ActionResult> ObterEstoqueBaixo(int unidadeId)
        {
            var estoqueBaixo = await _repository.ObterEstoqueBaixoAsync(unidadeId);
            return Ok(estoqueBaixo);
        }

        // GET: api/Estoques/{unidadeId}
        [HttpGet("{unidadeId}")]
        [Authorize(Roles = "Administrador,Gestor,Operador")] // Toda a equipe da unidade ou franqueadora pode consultar o saldo atual
        public async Task<ActionResult> ConsultarEstoqueUnidade(int unidadeId, [FromServices] GestaoFranquias.Api.Data.AppDbContext context)
        {
            // Consulta o saldo atual do estoque da unidade no banco
            var estoque = await context.Estoques
                .Where(e => e.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();

            if (estoque == null || !estoque.Any())
            {
                return NotFound(new { mensagem = "Nenhum estoque encontrado para esta unidade." });
            }

            return Ok(estoque);
        }
    }
}