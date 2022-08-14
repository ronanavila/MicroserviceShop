using Shop.WEB.Models;
using Shop.WEB.Services.Interfaces;
using System.Text;
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
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                _productsVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return _productsVM;
    }

    public async Task<ProductViewModel> GetProductById(int id)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                _productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return _productViewModel;
    }

    public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        StringContent content = new StringContent(JsonSerializer.Serialize(productViewModel), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                _productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return _productViewModel;
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        ProductViewModel productToUpdate = new ProductViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, productViewModel))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                productToUpdate = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }

        return productToUpdate;
    }

    public async Task<bool> DeleteProductById(int id)
    {
        var client = _httpClientFactory.CreateClient("ProductApi");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }
}
