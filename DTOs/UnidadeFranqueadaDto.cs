namespace GestaoFranquias.Api.DTOs
{
    // DTO utilizado para receber as informações essenciais ao cadastrar uma nova unidade franqueada
    public class UnidadeFranqueadaDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Responsavel { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
    }
}