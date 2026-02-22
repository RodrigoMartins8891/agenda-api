using Microsoft.AspNetCore.Mvc;
using AgendaApi2.Models;
using AgendaApi2.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AgendaApi2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConsultaController : ControllerBase
    {
        private readonly ConsultaService _service;

        public ConsultaController(ConsultaService service)
        {
            _service = service;
        }

        // GET: /api/consulta
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_service.Listar());
        }

        // GET: /api/consulta/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var consulta = _service.BuscarPorId(id);
            if (consulta == null) return NotFound();
            return Ok(consulta);
        }

        // POST: /api/consulta
        [HttpPost]
        public IActionResult Post(Consulta consulta)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            // ⚡ Atribui o UsuarioId do usuário logado
            consulta.UsuarioId = int.Parse(userIdClaim);

            var nova = _service.Criar(consulta);
            return Created("", nova);
        }

        // PUT: /api/consulta/{id}
        [HttpPut("{id}")]
        public IActionResult Put(int id, Consulta consulta)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();
            int idUsuario = int.Parse(userIdClaim);

            var existente = _service.BuscarPorId(id);
            if (existente == null) return NotFound();

            // ⚡ Somente dono pode atualizar
            if (existente.UsuarioId != idUsuario) return Forbid();

            if (!_service.Atualizar(id, consulta)) return NotFound();
            return NoContent();
        }

        // DELETE: /api/consulta/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();
            int idUsuario = int.Parse(userIdClaim);

            var existente = _service.BuscarPorId(id);
            if (existente == null) return NotFound();

            // ⚡ Somente dono pode deletar
            if (existente.UsuarioId != idUsuario) return Forbid();

            if (!_service.Remover(id)) return NotFound();
            return NoContent();
        }

        // GET: /api/consulta/minhas
        [HttpGet("minhas")]
        public IActionResult MinhasConsultas()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null) return Unauthorized();

            int idUsuario = int.Parse(userIdClaim);
            var consultas = _service.ListarPorUsuario(idUsuario);

            return Ok(consultas);
        }
    }
}
