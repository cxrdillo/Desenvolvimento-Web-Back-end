namespace GestaoFranquias.Api.DTOs
{
    // DTO estruturado para receber os dados de uma nova venda na franquia
    public class NovaVendaDto
    {
        public int UnidadeFranqueadaId { get; set; }
        public List<NovoItemVendaDto> Itens { get; set; } = new List<NovoItemVendaDto>();
    }

    // DTO auxiliar que representa cada produto e quantidade dentro da venda
    public class NovoItemVendaDto
    {
        public int ProdutoServicoId { get; set; }
        public int Quantidade { get; set; }
    }
}