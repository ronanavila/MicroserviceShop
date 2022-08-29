using Shop.DiscountApi.DTOs;

namespace Shop.DiscountApi.Repositories;

public interface ICouponRepository
{
    Task<CoupontDTO> GetCoupontByCode(string code);
}
