using GestaoFranquias.Api.Entities;

namespace GestaoFranquias.Api.Repositories
{
    // Interface que define os contratos de acesso a dados para o gerenciamento de unidades franqueadas
    public interface IUnidadeRepository
    {
        // Retorna a lista completa de todas as unidades cadastradas na rede
        Task<IEnumerable<UnidadeFranqueada>> ObterTodasAsync();

        // Busca uma unidade específica pelo seu identificador único
        Task<UnidadeFranqueada?> ObterPorIdAsync(int id);

        // Busca uma unidade pelo CNPJ para garantir a regra de unicidade no cadastro
        Task<UnidadeFranqueada?> ObterPorCnpjAsync(string cnpj);

        // Insere uma nova unidade franqueada na base de dados
        Task AdicionarAsync(UnidadeFranqueada unidade);

        // Atualiza as informações cadastrais de uma unidade existente
        Task AtualizarAsync(UnidadeFranqueada unidade);
    }
}