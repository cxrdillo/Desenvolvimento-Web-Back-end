using GestaoFranquias.Api.DTOs;
using GestaoFranquias.Api.Entities;
using GestaoFranquias.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GestaoFranquias.Api.Controllers
{
    [Authorize] // Exige autenticação via JWT para gerenciar as franquias
    [ApiController]
    [Route("api/[controller]")]
    public class UnidadesController : ControllerBase
    {
        private readonly IUnidadeRepository _repository;

        public UnidadesController(IUnidadeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/unidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadeFranqueada>>> ObterTodas()
        {
            var unidades = await _repository.ObterTodasAsync();
            return Ok(unidades);
        }

        // GET: api/unidades/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadeFranqueada>> ObterPorId(int id)
        {
            var unidade = await _repository.ObterPorIdAsync(id);
            if (unidade == null)
            {
                return NotFound(new { mensagem = "Unidade não encontrada." });
            }
            return Ok(unidade);
        }

        // POST: api/unidades
        [HttpPost]
        public async Task<ActionResult> Cadastrar(UnidadeFranqueadaDto dto)
        {
            // Regra de Negócio obrigatória: Impede cadastrar duas unidades com o mesmo CNPJ
            var unidadeExistente = await _repository.ObterPorCnpjAsync(dto.Cnpj);
            if (unidadeExistente != null)
            {
                return BadRequest(new { mensagem = "Já existe uma unidade cadastrada com este CNPJ." });
            }

            // Mapeia o DTO para a entidade de domínio
            var novaUnidade = new UnidadeFranqueada
            {
                Nome = dto.Nome,
                Cnpj = dto.Cnpj,
                Responsavel = dto.Responsavel,
                Contato = dto.Contato,
                Endereco = dto.Endereco,
                DataInicio = DateTime.UtcNow,
                Ativa = true,
                PercentualRoyalty = 5.0m // Percentual padrão exigido nas regras de negócio
            };

            await _repository.AdicionarAsync(novaUnidade);

            // Retorna 201 Created apontando para a rota de busca por ID
            return CreatedAtAction(nameof(ObterPorId), new { id = novaUnidade.Id }, novaUnidade);
        }
    }
}