﻿namespace Shop.CartApi.DTOs;

public class CartHeaderDTO
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CuponCode { get; set; } = string.Empty;
}
