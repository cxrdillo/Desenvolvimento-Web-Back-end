namespace GestaoFranquias.Api.Entities
{
    // Entidade que controla o saldo de estoque de um produto específico em determinada unidade
    public class Estoque
    {
        public int Id { get; set; }
        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada UnidadeFranqueada { get; set; } = null!;

        public int ProdutoServicoId { get; set; }
        public ProdutoServico ProdutoServico { get; set; } = null!;

        public int Quantidade { get; set; }
        public int QuantidadeMinima { get; set; } = 5; // Limite padrão para alertas de estoque crítico
    }
}