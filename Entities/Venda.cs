namespace GestaoFranquias.Api.Entities
{
    // Entidade que registra as vendas realizadas pelas unidades franqueadas
    public class Venda
    {
        public int Id { get; set; }
        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada UnidadeFranqueada { get; set; } = null!;

        public DateTime DataVenda { get; set; } = DateTime.UtcNow;
        public decimal ValorTotal { get; set; }

        public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    }

    // Entidade auxiliar que mapeia os itens individuais associados a uma venda
    public class ItemVenda
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public Venda Venda { get; set; } = null!;

        public int ProdutoServicoId { get; set; }
        public ProdutoServico ProdutoServico { get; set; } = null!;

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}