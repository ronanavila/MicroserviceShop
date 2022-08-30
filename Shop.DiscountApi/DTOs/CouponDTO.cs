using System.ComponentModel.DataAnnotations;

namespace Shop.DiscountApi.DTOs;

public class CouponDTO
{ 
    public int CouponId { get; set; }
    [Required]
    public string? CouponCode { get; set; }
    [Required]
    public decimal Discount { get; set; }
}
