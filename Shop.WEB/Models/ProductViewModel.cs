using System.ComponentModel.DataAnnotations;

namespace Shop.WEB.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public long Stock { get; set; }
    [Required]
    public string? ImageUrl { get; set; }
    public string? CategoryName { get; set; }
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    [Range(1, 100)]
    public int Quantity { get; set; } = 1;
}
