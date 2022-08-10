using Shop.ProductApi.Models;
using System.ComponentModel.DataAnnotations;

namespace Shop.ProductApi.DTOs;

public class ProductDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Price is required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [MinLength(5)]
    [MaxLength(250)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(1, 9999)]
    public long Stock { get; set; }

    public string? ImageUrl { get; set; }

    public Category? Category { get; set; }
    public int CategoryId { get; set; }
}
