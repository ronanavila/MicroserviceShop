namespace Shop.WEB.Models;

public class CartHeaderViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CuponCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; } = 0.00m;
}
