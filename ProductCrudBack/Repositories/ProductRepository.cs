using Microsoft.EntityFrameworkCore;
using ProductCrudBack.Data;
using ProductCrudBack.Models;

namespace ProductCrudBack.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ProductCrudDbContext context) 
        : base(context)
    {
    }
}