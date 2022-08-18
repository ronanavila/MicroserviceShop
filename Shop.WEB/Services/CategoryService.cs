using Shop.WEB.Models;
using Shop.WEB.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Shop.WEB.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly JsonSerializerOptions _option;
    private const string apiEndpoint = "/api/categories/";

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _option = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        IEnumerable<CategoryViewModel> categories;

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();

                categories = await JsonSerializer.DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _option);
            }
            else
            {
                return null;
            }
        }


        return categories;
    }
}
