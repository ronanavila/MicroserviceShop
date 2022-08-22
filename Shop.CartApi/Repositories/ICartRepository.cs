using Shop.CartApi.DTOs;

namespace Shop.CartApi.Repositories;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserIdAsync(string userID);
    Task<CartDTO> UpdateCartAsync(CartDTO cartDTO);
    Task<bool> CleanCartAsync(string userId);
    Task<bool> DeleteItemCartAsync(int carItemId);
    Task<bool> ApplyCouponAsync(string userId, string couponCode);
    Task<bool> DeleteCouponAsync(string userId);
}
