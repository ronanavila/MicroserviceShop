using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.DiscountApi.DTOs;
using Shop.DiscountApi.Repositories;

namespace Shop.DiscountApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiscountController : ControllerBase
{
    private ICouponRepository _repository;

    public DiscountController(ICouponRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<CoupontDTO>> GetDiscountCouponByCode(string code)
    {
        var coupon = await _repository.GetCoupontByCode(code);

        if (coupon is null)
            return NotFound($"Coupon Code: {coupon} not found.");

        return Ok(coupon);
    }
}
