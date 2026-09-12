using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace GestaoFranquias.Api.Controllers
{
    // Protege todas as rotas com JWT (exige token de acesso)
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChamadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChamadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Administrador,Gestor,Operador")] // Todo mundo na rede pode listar/consultar os chamados
        public async Task<ActionResult> ObterTodos() => Ok(await _context.ChamadosSuporte.ToListAsync());

        [HttpPost]
        [Authorize(Roles = "Administrador,Gestor,Operador")] // Unidades e gestores/operadores podem abrir chamados com a franqueadora
        public async Task<ActionResult> AbrirChamado(ChamadoSuporte chamado)
        {
            // Regra de negócio: define data automática e status inicial ao abrir o chamado
            chamado.DataAbertura = DateTime.UtcNow;
            chamado.Status = "Aberto";
            
            _context.ChamadosSuporte.Add(chamado);
            await _context.SaveChangesAsync();
            return Ok(chamado);
        }
    }
}