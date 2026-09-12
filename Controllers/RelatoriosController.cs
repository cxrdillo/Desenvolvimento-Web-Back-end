using GestaoFranquias.Api.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Controllers
{
    [Authorize] // Exige autenticação via JWT para acessar os indicadores gerenciais
    [ApiController]
    [Route("api/[controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RelatoriosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/relatorios/faturamento-unidade/{unidadeId}
        [HttpGet("faturamento-unidade/{unidadeId}")]
        [Authorize(Roles = "Administrador,Gestor")] // Apenas administradores e gestores visualizam relatórios de faturamento
        public async Task<ActionResult> FaturamentoPorUnidade(int unidadeId)
        {
            // Busca todas as vendas da unidade e calcula o faturamento total usando LINQ
            var vendas = await _context.Vendas
                .Where(v => v.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();

            var faturamentoTotal = vendas.Sum(v => v.ValorTotal);
            var totalVendas = vendas.Count;

            return Ok(new
            {
                unidadeId,
                totalVendas,
                faturamentoTotal
            });
        }

        // GET: api/relatorios/estoque-critico/{unidadeId}
        [HttpGet("estoque-critico/{unidadeId}")]
        [Authorize(Roles = "Administrador,Gestor")] // Relatório gerencial restrito à gestão e franqueadora
        public async Task<ActionResult> EstoqueCritico(int unidadeId)
        {
            // Filtra produtos cuja quantidade atual está abaixo ou igual ao estoque mínimo
            var itensCriticos = await _context.Estoques
                .Include(e => e.ProdutoServico)
                .Where(e => e.UnidadeFranqueadaId == unidadeId && e.Quantidade <= e.QuantidadeMinima)
                .Select(e => new
                {
                    produto = e.ProdutoServico.Nome,
                    quantidadeAtual = e.Quantidade,
                    quantidadeMinima = e.QuantidadeMinima
                })
                .ToListAsync();

            return Ok(itensCriticos);
        }
    }
}