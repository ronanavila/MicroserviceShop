using AutoMapper;
using Shop.ProductApi.DTOs;
using Shop.ProductApi.Models;
using Shop.ProductApi.Repositories;

namespace Shop.ProductApi.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
        var ProductsEntity = await _productRepository.GetAll();
        return _mapper.Map<IEnumerable<ProductDTO>>(ProductsEntity);
    }
 

    public async Task<ProductDTO> GetProductByID(int id)
    {
        var ProductEntity = await _productRepository.GetById(id);
        return _mapper.Map<ProductDTO>(ProductEntity);
    }

    public async Task AddProduct(ProductDTO ProductDTO)
    {
        var productEntity = _mapper.Map<Product>(ProductDTO);
        await _productRepository.Create(productEntity);
        ProductDTO.Id = productEntity.Id;
    }

    public async Task UpdateProduct(ProductDTO ProductDTO)
    {
        var productEntity = _mapper.Map<Product>(ProductDTO);
        await _productRepository.Update(productEntity);
    }

    public async Task RemoveProduct(int id)
    {
        var productEntity = _productRepository.GetById(id);
        await _productRepository.Delete(productEntity.Id);
    }
}
