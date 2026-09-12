using GestaoFranquias.Api.DTOs;
using GestaoFranquias.Api.Entities;

namespace GestaoFranquias.Api.Repositories
{
    // Interface que define os contratos de persistência e regras de negócio para as vendas das unidades
    public interface IVendaRepository
    {
        // Processa e registra uma nova venda, calculando totais e efetuando a baixa automática no estoque
        Task<Venda> RegistrarVendaAsync(NovaVendaDto dto);

        // Retorna o histórico de vendas realizadas por uma unidade específica
        Task<IEnumerable<Venda>> ObterVendasPorUnidadeAsync(int unidadeId);
    }
}