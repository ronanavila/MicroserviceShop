﻿namespace Shop.WEB.Models;

public class CartItemViewModel
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ProductId { get; set; }
    public int CartHeaderId { get; set; }
    public ProductViewModel? Product { get; set; }
}
