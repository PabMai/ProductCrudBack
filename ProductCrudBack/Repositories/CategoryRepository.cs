using ProductCrudBack.Data;
using ProductCrudBack.Models;

namespace ProductCrudBack.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ProductCrudDbContext context) 
            : base(context)
        {
        }
    }   
}