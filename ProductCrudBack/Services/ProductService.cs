using ProductCrudBack.Models;
using ProductCrudBack.Repositories;

namespace ProductCrudBack.Services;

public class ProductService : GenericService<Product>, IProductService
{
    public ProductService(IGenericRepository<Product> repository) : base(repository)
    {
        
    }
}