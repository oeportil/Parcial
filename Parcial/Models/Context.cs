using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Parcial.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { 

        }
    }
}
