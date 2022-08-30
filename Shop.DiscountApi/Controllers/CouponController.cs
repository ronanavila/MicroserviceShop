using Microsoft.AspNetCore.Mvc;
using Shop.DiscountApi.DTOs;
using Shop.DiscountApi.Repositories;

namespace Shop.DiscountApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CouponController : ControllerBase
{
    private  ICouponRepository _repository;

    public CouponController(ICouponRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{couponcode}")]
    public async Task<ActionResult<CouponDTO>> GetDiscountCouponByCode(string couponcode)
    {
        var coupon = await _repository.GetCoupontByCode(couponcode);

        if (coupon is null)
            return NotFound($"Coupon Code: {couponcode} not found.");

        return Ok(coupon);
    }
}
