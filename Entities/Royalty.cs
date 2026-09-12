namespace GestaoFranquias.Api.Entities
{
    // Entidade que armazena o cálculo mensal dos royalties devidos por cada unidade franqueada
    public class Royalty
    {
        public int Id { get; set; }
        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada UnidadeFranqueada { get; set; } = null!;

        public int MesReferencia { get; set; }
        public int AnoReferencia { get; set; }
        public decimal FaturamentoBase { get; set; }
        public decimal PercentualAplicado { get; set; }
        public decimal ValorDevido { get; set; }
        
        // "Pendente", "Pago"
        public string StatusPagamento { get; set; } = "Pendente";
        public DateTime DataCalculo { get; set; } = DateTime.UtcNow;
    }
}