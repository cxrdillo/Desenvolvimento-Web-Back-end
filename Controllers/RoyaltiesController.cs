using GestaoFranquias.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GestaoFranquias.Api.Controllers
{
    [Authorize] // Exige autenticação via token JWT para acessar os cálculos financeiros
    [ApiController]
    [Route("api/[controller]")]
    public class RoyaltiesController : ControllerBase
    {
        private readonly IRoyaltyRepository _repository;

        public RoyaltiesController(IRoyaltyRepository repository)
        {
            _repository = repository;
        }

        // POST: api/royalties/calcular
        [HttpPost("calcular")]
        public async Task<ActionResult> CalcularRoyalty(int unidadeId, int mes, int ano)
        {
            try
            {
                // Dispara o cálculo do royalty com base no faturamento da unidade no período
                var royalty = await _repository.CalcularRoyaltyAsync(unidadeId, mes, ano);
                return Ok(royalty);
            }
            catch (Exception ex)
            {
                // Tratamento de exceção para garantir retorno HTTP coerente
                return BadRequest(new { erro = ex.Message });
            }
        }

        // GET: api/royalties/unidade/{unidadeId}
        [HttpGet("unidade/{unidadeId}")]
        public async Task<ActionResult> ObterPorUnidade(int unidadeId)
        {
            // Consulta os registros de royalties e faturamento gerados para a unidade
            var royalties = await _repository.ObterRoyaltiesPorUnidadeAsync(unidadeId);
            return Ok(royalties);
        }
    }
}