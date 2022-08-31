using Microsoft.AspNetCore.Mvc;
using Shop.CartApi.DTOs;
using Shop.CartApi.Repositories;

namespace Shop.CartApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartRepository _cartRepository;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    [HttpGet("getcart/{userid}")]
    public async Task<ActionResult<CartDTO>> GetByUserID(string userid)
    {
        var cartDTO = await _cartRepository.GetCartByUserIdAsync(userid);

        if (cartDTO is null)
            return NotFound();

        return Ok(cartDTO);
    }

    [HttpPost("addcart")]
    public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCartAsync(cartDTO);

        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpPut("updatecart")]
    public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
    {
        var cart = await _cartRepository.UpdateCartAsync(cartDTO);

        if (cart is null)
            return NotFound();

        return Ok(cart);
    }

    [HttpDelete("deletecart/{id}")]
    public async Task<ActionResult<bool>> DeleteCart(int id)
    {
        var status = await _cartRepository.DeleteItemCartAsync(id);

        if (!status)
            return BadRequest();

        return Ok(status);
    }

    [HttpPost("applycoupon")]
    public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
    {
        var result = await _cartRepository.ApplyCouponAsync(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);

        if (!result)
        {
            return NotFound($"CartHeader not found for User: {cartDTO.CartHeader.UserId}.");
        }

        return Ok(result);
    }

    [HttpDelete("deletecoupon/{userId}")]
    public async Task<ActionResult<CartDTO>> DeleteCoupon(string userId)
    {
        var result = await _cartRepository.DeleteCouponAsync(userId);
        if (!result)
        {
            return NotFound($"Discount not found for User: {userId}");
        }

        return Ok(result);
    }

    [HttpPost("checkout")]
    public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO checkoutDTO)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(checkoutDTO.UserId);

        if (cart is null)
            return NotFound($"Cart Not Found {checkoutDTO.UserId}");

        checkoutDTO.CartItems = cart.CartItems;
        checkoutDTO.DateTime = DateTime.Now;

        return Ok(checkoutDTO);
    }
}
