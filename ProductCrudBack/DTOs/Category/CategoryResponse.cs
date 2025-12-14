namespace ProductCrudBack.DTOs.Category;

public class CategoryResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    public CategoryResponse()
    {
    }
    
    public CategoryResponse(Models.Category category)
    {
        Id = category.Id;
        Name = category.Name;
    }

    public IEnumerable<CategoryResponse> ToEnumerable(IEnumerable<Models.Category>  categories)
    {
        return categories.Select(x => new CategoryResponse
        {
            Id = x.Id,
            Name = x.Name
        } ).ToList();
    }
}