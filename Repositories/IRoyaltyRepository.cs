using GestaoFranquias.Api.Entities;

namespace GestaoFranquias.Api.Repositories
{
    // Interface que define o contrato para os cálculos e consultas de royalties das franquias
    public interface IRoyaltyRepository
    {
        // Processa o faturamento mensal da unidade e calcula o valor devido de royalties com base no percentual contratual
        Task<Royalty> CalcularRoyaltyAsync(int unidadeId, int mes, int ano);

        // Consulta o histórico de royalties calculados e registrados para uma unidade específica
        Task<IEnumerable<Royalty>> ObterRoyaltiesPorUnidadeAsync(int unidadeId);
    }
}