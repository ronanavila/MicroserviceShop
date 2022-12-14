using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.DiscountApi.Context;
using Shop.DiscountApi.DTOs;

namespace Shop.DiscountApi.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _context;
    private IMapper _mapper;

    public CouponRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDTO> GetCoupontByCode(string couponcode)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponcode);
        return _mapper.Map<CouponDTO>(coupon);
    }
}
