using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestaoFranquias.Api.Data;
using GestaoFranquias.Api.DTOs;
using GestaoFranquias.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace GestaoFranquias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        // Injetando o banco e as configurações para acessar a chave JWT do appsettings
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Rota para cadastrar novos usuários no sistema com perfis específicos (Admin, Gestor, Operador)
        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegistrarUsuarioDto dto)
        {
            // Validação de negócio: impede o cadastro duplicado de e-mails
            var existe = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (existe) return BadRequest(new { erro = "Já existe um usuário cadastrado com este e-mail." });

            // Validação opcional de perfil para garantir consistência nos papéis da rede
            var perfilValido = dto.Perfil == "Administrador" || dto.Perfil == "Gestor" || dto.Perfil == "Operador";
            if (!perfilValido)
                dto.Perfil = "Operador"; // Padrão de segurança caso mandem perfil inválido

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = dto.Senha,
                Perfil = dto.Perfil,
                Ativo = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
        }

        // Rota de Login que autentica e emite o token JWT contendo as Roles e permissões de acesso
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.SenhaHash == dto.Senha && u.Ativo);

            if (usuario == null)
                return Unauthorized(new { erro = "E-mail ou senha inválidos, ou usuário inativo." });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "ChavePadraoMuitoSegura123456");
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Injetando as Claims essenciais para o controle de acesso (Roles e Identificação)
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Perfil), // Essencial para o [Authorize(Roles = "...")]
                    new Claim("NomeUsuario", usuario.Nome)
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new 
            { 
                token = tokenString, 
                perfil = usuario.Perfil, 
                nome = usuario.Nome,
                mensagem = $"Bem-vindo, {usuario.Nome}! Acesso autenticado como {usuario.Perfil}." 
            });
        }
    }
}