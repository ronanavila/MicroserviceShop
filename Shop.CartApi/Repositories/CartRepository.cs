using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shop.CartApi.Context;
using Shop.CartApi.DTOs;
using Shop.CartApi.Models;
using System.Xml.Linq;

namespace Shop.CartApi.Repositories;

public class CartRepository : ICartRepository
{
    private readonly AppDbContext _appDbContext;
    private IMapper _mapper;

    public CartRepository(AppDbContext appDbContext, IMapper mapper)
    {
        _appDbContext = appDbContext;
        _mapper = mapper;
    }

    public async Task<CartDTO> GetCartByUserIdAsync(string userID)
    {
        Cart cart = new Cart
        {
            CartHeader = await _appDbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userID)
        };

        cart.CartItems = _appDbContext.CartItems.Where(c => c.CartHeaderId == cart.CartHeader.Id)
            .Include(c => c.Product);

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> DeleteItemCartAsync(int carItemId)
    {
        try
        {
            CartItem cartItem = await _appDbContext.CartItems.FirstOrDefaultAsync(c => c.Id == carItemId);

            int totalItems = _appDbContext.CartItems.Where(c => c.CartHeaderId == cartItem.CartHeaderId).Count();

            _appDbContext.CartItems.Remove(cartItem);

            await _appDbContext.SaveChangesAsync();

            if (totalItems == 1)
            {
                var cartHeaderRemove = await _appDbContext.CartHeaders.FirstOrDefaultAsync(x => x.Id == cartItem.CartHeaderId);
                _appDbContext.CartItems.Remove(cartItem);
                await _appDbContext.SaveChangesAsync();
            }
            
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public async Task<bool> CleanCartAsync(string userId)
    {
        var cartHeader = await _appDbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);

        if (cartHeader is not null)
        {
            _appDbContext.CartItems.RemoveRange(_appDbContext.CartItems.Where(c => c.CartHeaderId == cartHeader.Id));
            _appDbContext.CartHeaders.Remove(cartHeader);

            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<CartDTO> UpdateCartAsync(CartDTO cartDTO)
    {
        Cart cart = _mapper.Map<Cart>(cartDTO);

        await SaveProductInDatabase(cartDTO, cart);

        var cartHeader = await _appDbContext.CartHeaders.AsNoTracking()
            .FirstOrDefaultAsync(c => c.UserId == cart.CartHeader.UserId);

        if (cartHeader is null)
        {
            await CreateCartHeaderAndItems(cart);
        }
        else
        {
            await UpdateQuantityAndItems(cartDTO, cart, cartHeader);
        }

        return _mapper.Map<CartDTO>(cart);
    }

    public async Task<bool> ApplyCouponAsync(string userId, string couponCode)
    {
        var cartHeaderApplyCoupon = await _appDbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
        if(cartHeaderApplyCoupon is not null)
        {
            cartHeaderApplyCoupon.CouponCode = couponCode;

            _appDbContext.CartHeaders.Update(cartHeaderApplyCoupon);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteCouponAsync(string userId)
    {
        var cartHeaderDeleteCoupon = await _appDbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
        if (cartHeaderDeleteCoupon is not null)
        {
            cartHeaderDeleteCoupon.CouponCode = string.Empty;

            _appDbContext.CartHeaders.Update(cartHeaderDeleteCoupon);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }


    private async Task SaveProductInDatabase(CartDTO cartDTO, Cart cart)
    {
        var product = await _appDbContext.Products.FirstOrDefaultAsync(p =>
        p.Id == cartDTO.CartItems.FirstOrDefault().ProductId);

        if (product is null)
        {
            _appDbContext.Products.Add(cart.CartItems.FirstOrDefault().Product);
            await _appDbContext.SaveChangesAsync();
        }
    }

    private async Task CreateCartHeaderAndItems(Cart cart)
    {
        _appDbContext.CartHeaders.Add(cart.CartHeader);
        await _appDbContext.SaveChangesAsync();

        cart.CartItems.FirstOrDefault().CartHeaderId = cart.CartHeader.Id;
        cart.CartItems.FirstOrDefault().Product = null;

        _appDbContext.CartItems.Add(cart.CartItems.FirstOrDefault());

        await _appDbContext.SaveChangesAsync();
    }

    private async Task UpdateQuantityAndItems(CartDTO cartDTO, Cart cart, CartHeader? cartHeader)
    {
        var cartDetail = await _appDbContext.CartItems.AsNoTracking()
            .FirstOrDefaultAsync(p => p.ProductId == cartDTO.CartItems.FirstOrDefault()
            .ProductId && p.CartHeaderId == cartHeader.Id);

        if(cartDetail is null)
        {
            cart.CartItems.FirstOrDefault().CartHeaderId = cartHeader.Id;
            cart.CartItems.FirstOrDefault().Product = null;
            _appDbContext.CartItems.Add(cart.CartItems.FirstOrDefault());
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            cart.CartItems.FirstOrDefault().Product = null;
            cart.CartItems.FirstOrDefault().Quantity += cartDetail.Quantity;
            cart.CartItems.FirstOrDefault().Id = cartDetail.Id;
            cart.CartItems.FirstOrDefault().CartHeaderId = cartDetail.CartHeaderId;
            _appDbContext.CartItems.Update(cart.CartItems.FirstOrDefault());
            await _appDbContext.SaveChangesAsync();
        }
    }
}
