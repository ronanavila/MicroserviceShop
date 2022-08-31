﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.WEB.Models;
using Shop.WEB.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace Shop.WEB.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ICouponService _couponService;
    public CartController(ICartService cartService, ICouponService couponService)
    {
        _cartService = cartService;
        _couponService = couponService;
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

    [Authorize]
    private async Task<CartViewModel?> GetCartByUser()
    {
        var token = await GetAccessToken();
        var cart = await _cartService.GetCartByUserIdAsync(GetUserId(), token);

        if (cart?.CartHeader is not null)
        {
            if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
            {
                var coupon = await _couponService.GetDiscountCoupon(cart.CartHeader.CouponCode, token);

                if (coupon?.CouponCode is not null)
                    cart.CartHeader.Discount = coupon.Discount;
            }

            foreach (var item in cart.CartItems)
            {
                cart.CartHeader.TotalAmount += item.Product.Price * item.Quantity;
            }

            cart.CartHeader.TotalAmount -= (cart.CartHeader.TotalAmount * cart.CartHeader.Discount) / 100;
        }
        return cart;
    }

    [HttpPost]
    public async Task<IActionResult> ApplyCoupon(CartViewModel cartView)
    {
        if (ModelState.IsValid)
            await _cartService.ApplyCouponAsync(cartView, await GetAccessToken());

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCoupon()
    {
        if (ModelState.IsValid)
        {
            var result = await _cartService.RemoveCouponAsync(GetUserId(), await GetAccessToken());

            if (result)
                return RedirectToAction(nameof(Index));
        }

        return View();
    }
    public async Task<IActionResult> RemoveItem(int id)
    {
        var result = await _cartService.RemoveItemFromCartAsync(id, await GetAccessToken());
        if (result)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(id);
    }
    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
        CartViewModel? cartVM = await GetCartByUser();
        return View(cartVM);
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
