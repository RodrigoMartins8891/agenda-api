using Microsoft.AspNetCore.Mvc;
using AgendaApi2.Data;
using AgendaApi2.Models;
using AgendaApi2.Services;
using System.Linq;


namespace AgendaApi2.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AgendaContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AgendaContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login(Usuario login)
        {
            var usuario = _context.Usuarios
                .FirstOrDefault(u => u.Email == login.Email);

            if (usuario == null)
                return Unauthorized("Usuário inválido");

            bool senhaValida = BCrypt.Net.BCrypt.Verify(login.Senha, usuario.Senha);

            if (!senhaValida)
                return Unauthorized("Usuário inválido");

            var token = _tokenService.GerarToken(usuario);

            return Ok(new { token });
        }

    }
}
