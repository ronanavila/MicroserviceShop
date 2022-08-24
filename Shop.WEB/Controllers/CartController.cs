using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.WEB.Models;
using Shop.WEB.Services.Interfaces;

namespace Shop.WEB.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
      
    [Authorize]
    public async Task<IActionResult> Index()
    {
        CartViewModel? cartView = await GetCartByUser();

        if (cartView is null)
        {
            ModelState.AddModelError("CartNotFound", "Cart does not exist, start shopping");
            return View("/Views/Cart/CartNotFound.cshtml");
        } 
            
        return View(cartView);
    }

    private async Task<CartViewModel?> GetCartByUser()
    {
        var cart = await _cartService.GetCartByUserIdAsync(GetUserId(), await GetAccessToken());

        if(cart?.CartHeader is not null)
        {
            foreach(var item in cart.CartItems)
            {
                cart.CartHeader.TotalAmount += item.Product.Price * item.Quantity;
            }
        }
        return cart;
    }

    private async Task<string> GetAccessToken()
    {
        return await HttpContext.GetTokenAsync("access_token");
    }

    private string GetUserId()
    {
        return User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value;
    }
}
