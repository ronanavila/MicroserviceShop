using Shop.WEB.Models;

namespace Shop.WEB.Services.Interfaces;

public interface ICartService
{
    Task<CartViewModel> GetCartByUserIdAsync(string userId, string token);
    Task<CartViewModel> AddItemToCartAsync(CartViewModel cartView, string token);
    Task<CartViewModel> UpdateCartAsync(CartViewModel cartView, string token);
    Task<bool> RemoveItemFromCartAsync(int cartId, string token);

    Task<bool> ApplyCouponAsync(CartViewModel cartView,string couponCode, string token);
    Task<bool> RemoveCouponAsync(string userId, string token);
    Task<bool> ClearCartAsync(string userId, string token);

    Task<CartViewModel> CheckoutAsync(CartHeaderViewModel cartHeader, string token);
}
