using Microsoft.EntityFrameworkCore;
using AgendaApi2.Models;

namespace AgendaApi2.Data
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options)
            : base(options)
        {
        }

        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

    }
}
