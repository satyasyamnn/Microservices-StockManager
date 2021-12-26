using Microsoft.EntityFrameworkCore;
using Products.Api.Models;

namespace Products.Api
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options): base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(c => c.Code);                                  
        }
    }
}
