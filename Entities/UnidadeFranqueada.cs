namespace GestaoFranquias.Api.Entities
{
    // Entidade principal que representa uma unidade franqueada na rede
    public class UnidadeFranqueada
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty; // Único no sistema (validado no banco)
        public string Responsavel { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; } = DateTime.UtcNow;
        public bool Ativa { get; set; } = true;

        // Percentual padrão de royalty cobrado pela franqueadora dessa unidade (ex: 5.0 para 5%)
        public decimal PercentualRoyalty { get; set; } = 5.0m;

        // Relacionamentos do EF Core (Navegação para tabelas filhas)
        public ICollection<Venda> Vendas { get; set; } = new List<Venda>();
        public ICollection<Estoque> Estoques { get; set; } = new List<Estoque>();
        public ICollection<ChamadoSuporte> Chamados { get; set; } = new List<ChamadoSuporte>();
    }
}