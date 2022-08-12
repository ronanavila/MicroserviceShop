using Shop.WEB.Models;
using Shop.WEB.Services.Interfaces;
using System.Text.Json;

namespace Shop.WEB.Services;

public class ProductService : IProductService
{
    private readonly JsonSerializerOptions _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private const string apiEndpoint = "/api/products/";
    private ProductViewModel _productViewModel;
    private IEnumerable<ProductViewModel> _productsVM;


    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true};
    }
    public Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
    {
        throw new NotImplementedException();
    }

    public Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductById()
    {
        throw new NotImplementedException();
    }
}
