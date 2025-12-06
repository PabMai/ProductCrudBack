using ProductCrudBack.Models;
using ProductCrudBack.Repositories;

namespace ProductCrudBack.Services;

public class CategoryService : GenericService<Category>, ICategoryService
{
    public CategoryService(IGenericRepository<Category> repository) : base(repository)
    {
        
    }
}