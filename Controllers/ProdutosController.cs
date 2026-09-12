using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Controllers
{
    [Authorize] // Exige token JWT para liberar o acesso ao catálogo
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Lista todos os produtos e serviços do catálogo centralizado
        [HttpGet]
        [Authorize(Roles = "Administrador,Gestor,Operador")] // Toda a rede pode consultar o catálogo de itens padronizados
        public async Task<ActionResult> ObterTodos() => Ok(await _context.ProdutosServicos.ToListAsync());

        // POST: Cadastra novo produto ou serviço padrão
        [HttpPost]
        [Authorize(Roles = "Administrador")] // Apenas a Franqueadora define o catálogo padrão de produtos e serviços
        public async Task<ActionResult> CadastrarProduto([FromBody] ProdutoServico produto)
        {
            // Salva o novo produto ou serviço padrão da rede no banco
            _context.ProdutosServicos.Add(produto);
            await _context.SaveChangesAsync();
            return Ok(produto);
        }
    }
}