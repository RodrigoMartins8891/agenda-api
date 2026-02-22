using AgendaApi2.Data;
using AgendaApi2.Models;
using System.Collections.Generic;
using System.Linq;

namespace AgendaApi2.Services   // usa o mesmo namespace do projeto
{
    public class ConsultaService
    {
        private readonly AgendaContext _context;

        public ConsultaService(AgendaContext context)
        {
            _context = context;
        }

        public List<Consulta> Listar()
        {
            return _context.Consultas.ToList();
        }

        public Consulta? BuscarPorId(int id)
        {
            return _context.Consultas.Find(id);
        }

        public Consulta Criar(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
            _context.SaveChanges();
            return consulta;
        }

        public bool Atualizar(int id, Consulta nova)
        {
            var existente = _context.Consultas.Find(id);
            if (existente == null) return false;

            existente.Paciente = nova.Paciente;
            existente.Medico = nova.Medico;
            existente.Data = nova.Data;

            _context.SaveChanges();
            return true;
        }

        public bool Remover(int id)
        {
            var consulta = _context.Consultas.Find(id);
            if (consulta == null) return false;

            _context.Consultas.Remove(consulta);
            _context.SaveChanges();
            return true;
        }

        public List<Consulta> ListarPorUsuario(int usuarioId)
        {
            return _context.Consultas
                .Where(c => c.UsuarioId == usuarioId)
                .ToList();
        }

    }
}
