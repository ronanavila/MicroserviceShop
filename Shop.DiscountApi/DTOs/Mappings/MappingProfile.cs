using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Shop.DiscountApi.Models;

namespace Shop.DiscountApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Coupon, CouponDTO>().ReverseMap();
    }
}
