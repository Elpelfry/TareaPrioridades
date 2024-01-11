using Microsoft.EntityFrameworkCore;
using TareaPrioridades.Models;

namespace TareaPrioridades.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Prioridades> Prioridades { get; set; }

        public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    }
}
