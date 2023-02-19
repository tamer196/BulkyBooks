using BulkyBooks.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBooks.DataAccess
{
    public class BulkyDbContext : DbContext
    {

        public BulkyDbContext(DbContextOptions<BulkyDbContext> options) : base(options)
        {

        }


        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
