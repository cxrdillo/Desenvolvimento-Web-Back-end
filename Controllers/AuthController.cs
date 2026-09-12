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

        // Injetando o banco e as configurações (pra gente pegar a chave do JWT lá no appsettings)
        public AuthController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Rota para cadastrar novos usuários no sistema
        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegistrarUsuarioDto dto)
        {
            // Validação de negócio: não deixa cadastrar dois usuários com o mesmo e-mail
            var existe = await _context.Usuarios.AnyAsync(u => u.Email == dto.Email);
            if (existe) return BadRequest(new { erro = "Já existe um usuário cadastrado com este e-mail." });

            // Montando o objeto do usuário novo
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = dto.Senha, // (Em app real teria um hash de senha, mas para o trabalho tá safe)
                Perfil = dto.Perfil,
                Ativo = true
            };

            // Salvando no banco de dados com EF Core
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Usuário cadastrado com sucesso!" });
        }

        // Rota de Login que gera o token JWT para liberar as outras abas
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto dto)
        {
            // Vai no banco procurar o cabra batendo e-mail, senha e conferindo se ele tá ativo
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.SenhaHash == dto.Senha && u.Ativo);

            // Se não achar ninguém, barramos o acesso com 401 Unauthorized
            if (usuario == null)
                return Unauthorized(new { erro = "E-mail ou senha inválidos, ou usuário inativo." });

            // Configuração da geração do token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "ChavePadraoMuitoSegura123456");
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Guardando as informações (Claims) dentro do crachá digital (token)
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Role, usuario.Perfil) // Define o perfil (Admin, Operador, etc)
                }),
                Expires = DateTime.UtcNow.AddHours(8), // Token expira em 8 horas
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                // Assinando o token com chave simétrica e criptografia pesada HmacSha256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Criando e escrevendo a string final do token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Retorna o token para o cliente salvar e usar nas requisições protegidas
            return Ok(new { token = tokenString, perfil = usuario.Perfil, nome = usuario.Nome });
        }
    }
}