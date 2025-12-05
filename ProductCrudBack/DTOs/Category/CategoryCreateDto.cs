using System.ComponentModel.DataAnnotations;

namespace ProductCrudBack.DTOs.Category;

public class CategoryCreateDto
{
    [Required]
    public string Name { get; set; }
}