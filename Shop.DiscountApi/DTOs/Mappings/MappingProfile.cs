using AutoMapper;
using Shop.DiscountApi.Models;

namespace Shop.DiscountApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    MappingProfile()
    {
        CreateMap<CoupontDTO, Coupon>().ReverseMap();
    }
}
