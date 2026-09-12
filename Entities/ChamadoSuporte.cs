namespace GestaoFranquias.Api.Entities
{
    // Entidade que representa os chamados de suporte abertos pelas franquias
    public class ChamadoSuporte
    {
        public int Id { get; set; }
        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada? UnidadeFranqueada { get; set; }
        public string Categoria { get; set; } = string.Empty; // Ex: Manutenção, Financeiro, TI
        public string Prioridade { get; set; } = "Normal"; // Baixa, Normal, Alta
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = "Aberto"; // Aberto, Em Andamento, Encerrado
        public DateTime DataAbertura { get; set; } = DateTime.UtcNow;
    }
}