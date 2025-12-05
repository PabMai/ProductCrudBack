using System.ComponentModel.DataAnnotations;

namespace ProductCrudBack.DTOs.Category;

public class CategoryUpdateDto
{
    [Required]
    public string Name { get; set; }
}