using System.ComponentModel.DataAnnotations;

namespace ProductCrudBack.DTOs.Product;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; } = "";
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}