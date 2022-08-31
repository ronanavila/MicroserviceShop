﻿namespace Shop.WEB.Models;

public class CartHeaderViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; } = 0.00m;
    //Discount
    public decimal Discount { get; set; } = 0.00m;
    //Checkout
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    //CreditCard
    public string CardNumber { get; set; } = string.Empty;
    public string NameOnCard { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
    public string ExpireDate { get; set; } = string.Empty;
}
