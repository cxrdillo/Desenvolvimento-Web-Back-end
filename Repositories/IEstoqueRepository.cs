using GestaoFranquias.Api.Entities;

namespace GestaoFranquias.Api.Repositories
{
    // Interface que define o contrato para as operações de controle de estoque e repositório
    public interface IEstoqueRepository
    {
        // Retorna a lista de produtos com estoque abaixo do limite mínimo para alertas
        Task<IEnumerable<Estoque>> ObterEstoqueBaixoAsync(int unidadeId);

        // Realiza entradas ou saídas de itens no estoque com validação de saldo
        Task MovimentarEstoqueAsync(int unidadeId, int produtoId, int quantidade);
    }
}