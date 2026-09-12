using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GestaoFranquias.Api.Controllers
{
    // Exige autenticação via token JWT em todas as rotas do controlador
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FornecedoresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Retorna a lista completa de fornecedores cadastrados
        [HttpGet]
        [Authorize(Roles = "Administrador,Gestor,Operador")] // Toda a rede pode consultar os parceiros homologados
        public async Task<ActionResult> ObterTodos() => Ok(await _context.Fornecedores.ToListAsync());

        // POST: Cadastra um novo fornecedor no banco de dados
        [HttpPost]
        [Authorize(Roles = "Administrador")] // Apenas o Administrador da Franqueadora homologa/cadastra novos fornecedores
        public async Task<ActionResult> Cadastrar(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
            return Ok(fornecedor);
        }
    }
}