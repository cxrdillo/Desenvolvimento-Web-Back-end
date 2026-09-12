namespace GestaoFranquias.Api.DTOs
{
    // DTO para carregar os dados necessários ao registrar um novo usuário
    public class RegistrarUsuarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public string Perfil { get; set; } = "Operador"; // Perfil padrão caso não seja informado
    }

    // DTO focado estritamente nas credenciais de acesso
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}