using API_NASAT.Models;
using Microsoft.EntityFrameworkCore;

namespace API_NASAT.Context
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<DbUsers> Usuarios { get; set; }
    }
}
