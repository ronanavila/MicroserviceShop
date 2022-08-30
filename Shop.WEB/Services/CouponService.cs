using Shop.WEB.Models;
using Shop.WEB.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Shop.WEB.Services;

public class CouponService : ICouponService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/coupon";
    private CouponViewModel _couponViewModel = new CouponViewModel();

    public CouponService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<CouponViewModel> GetDiscountCoupon(string couponCode, string token)
    {
        var client = _httpClientFactory.CreateClient("DiscountApi");
        PutTokenInHeaderAuthorization(token, client);
        using (var response = await client.GetAsync($"{apiEndpoint}/{couponCode}"))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                _couponViewModel = await JsonSerializer.DeserializeAsync<CouponViewModel>(apiResponse, _options);
            }
            else
            {
                return null;
            }
            return _couponViewModel;
        }

    }
    private void PutTokenInHeaderAuthorization(string token, HttpClient client)
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
