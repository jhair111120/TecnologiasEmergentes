using CRM.Entidades;
using Microsoft.EntityFrameworkCore;

namespace CRM
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contacto> Contactos { get; set; }
    }
}
