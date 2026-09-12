using GestaoFranquias.Api.DTOs;
using GestaoFranquias.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestaoFranquias.Api.Controllers
{
    [Authorize] // Exige token JWT para registrar vendas
    [ApiController]
    [Route("api/[controller]")]
    public class VendasController : ControllerBase
    {
        private readonly IVendaRepository _repository;

        public VendasController(IVendaRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> RegistrarVenda(NovaVendaDto dto)
        {
            try
            {
                // Processa a venda, calcula o total e abate automaticamente o estoque
                var venda = await _repository.RegistrarVendaAsync(dto);
                return Ok(venda);
            }
            catch (Exception ex)
            {
                // Retorna 400 Bad Request se falhar alguma validação (ex: estoque insuficiente ou unidade inativa)
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}