namespace Shop.WEB.Models;

public class CouponViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; } = 0.00m;
    public decimal Discount { get; set; } = 0.00m;
}
