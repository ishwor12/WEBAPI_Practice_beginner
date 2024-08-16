using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Practice_web_api
{

    public class ApplicationDbContext : DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
