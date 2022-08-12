using AutoMapper;
using Shop.ProductApi.Models;

namespace Shop.ProductApi.DTOs.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<ProductDTO, Product>();
        CreateMap<Product, ProductDTO>().ForMember(x => x.CategoryName, opt => opt.MapFrom(c => c.Category.Name));
    }
}
