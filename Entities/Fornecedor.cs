namespace GestaoFranquias.Api.Entities
{
    // Entidade que representa os fornecedores parceiros cadastrados no sistema
    public class Fornecedor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
    }
}