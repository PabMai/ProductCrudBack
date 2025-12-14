namespace ProductCrudBack.DTOs.Product;

public class ProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public int CategoryId { get; set; }

    public ProductResponse()
    {
    }
    
    public ProductResponse(Models.Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        CategoryId = product.CategoryId;
    }

    public IEnumerable<ProductResponse> ToEnumerable(IEnumerable<Models.Product>  products)
    {
        return products.Select(x => new ProductResponse
        {
            Id = x.Id,
            Name = x.Name, 
            Price = x.Price, 
            CategoryId = x.CategoryId
        } ).ToList();
    }
}