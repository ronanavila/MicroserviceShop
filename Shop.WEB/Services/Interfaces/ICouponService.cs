using Shop.WEB.Models;

namespace Shop.WEB.Services.Interfaces;

public interface ICouponService
{
    Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token);
}
