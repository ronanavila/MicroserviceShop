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

    [HttpGet("getcart/{id}")]
    public async Task<ActionResult<CartDTO>> GetByUserID(string Id)
    {
        var cartDTO = await _cartRepository.GetCartByUserIdAsync(Id);

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
    public async Task<ActionResult<bool>> DeleteCart(int Id)
    {
        var status = await _cartRepository.DeleteItemCartAsync(Id);

        if (!status)
            return BadRequest();

        return Ok(status);
    }
}
