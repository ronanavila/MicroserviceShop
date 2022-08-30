using Shop.DiscountApi.DTOs;

namespace Shop.DiscountApi.Repositories;

public interface ICouponRepository
{
    Task<CouponDTO> GetCoupontByCode(string code);
}
