using Microsoft.EntityFrameworkCore;
using ProductCrudBack.Models;

namespace ProductCrudBack.Data
{
    public class ProductCrudDbContext : DbContext
    {
        public ProductCrudDbContext(DbContextOptions<ProductCrudDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
    }    
}