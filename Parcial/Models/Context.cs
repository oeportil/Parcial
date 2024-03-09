using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PARCIAL_1A.Models;

namespace PARCIAL_1A.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { 
                
        }
        public virtual DbSet<AutorLibro> AutorLibros { get; set; }

        public virtual DbSet<Autore> Autores { get; set; }

        public virtual DbSet<Libro> Libros { get; set; }

        public virtual DbSet<Post> Posts { get; set; }
    }
}
