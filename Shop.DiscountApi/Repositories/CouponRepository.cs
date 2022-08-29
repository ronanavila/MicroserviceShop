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

    public async Task<CoupontDTO> GetCoupontByCode(string code)
    {
        var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponCode == code);
        return _mapper.Map<CoupontDTO>(coupon);
    }
}
