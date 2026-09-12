namespace GestaoFranquias.Api.Entities
{
    // Entidade que representa os usuários do sistema com acesso autenticado via JWT
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; // Único no sistema (validado no banco)
        public string SenhaHash { get; set; } = string.Empty; // Armazena a senha criptografada em hash
        public string Perfil { get; set; } = "Operador"; // Ex: Admin, Gestor, Operador
        public bool Ativo { get; set; } = true;
    }
}