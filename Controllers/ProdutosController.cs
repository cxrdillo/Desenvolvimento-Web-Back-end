using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestaoFranquias.Api.Controllers
{
    [Authorize] // Exige token JWT para liberar o cadastro no catálogo
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarProduto([FromBody] ProdutoServico produto)
        {
            // Salva o novo produto ou serviço padrão da rede no banco
            _context.ProdutosServicos.Add(produto);
            await _context.SaveChangesAsync();
            return Ok(produto);
        }
    }
}