using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PARCIAL_1A.Models;

namespace PARCIAL_1A.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { 
                
        }
        public virtual DbSet<AutorLibro> AutorLibro { get; set; }

        public virtual DbSet<Autores> Autores { get; set; }

        public virtual DbSet<Libros> Libros { get; set; }

        public virtual DbSet<Posts> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutorLibro>().HasNoKey();
        }
    }
}
