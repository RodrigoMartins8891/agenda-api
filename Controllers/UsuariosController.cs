using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using AgendaApi2.Data;
using AgendaApi2.Models;
using BCrypt.Net;

namespace AgendaApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AgendaContext _context;

        public UsuariosController(AgendaContext context)
        {
            _context = context;
        }

        // ➤ Criar usuário
        [HttpPost("register")]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            // Hash da senha
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok(new { usuario.Id, usuario.Email });
        }

        // ➤ Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (usuario == null)
                return Unauthorized("Usuário ou senha inválidos");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.Senha);

            if (!senhaValida)
                return Unauthorized("Usuário ou senha inválidos");

            var token = GerarToken(usuario);

            return Ok(new { token });
        }

        // ➤ Método para gerar JWT
        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("CHAVE_SUPER_SECRETA_123456_7890_ABC"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "api",
                audience: "api",
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
