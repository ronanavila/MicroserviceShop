using Microsoft.EntityFrameworkCore;
using Shop.ProductApi.Context;
using Shop.ProductApi.Models;

namespace Shop.ProductApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }


    public async Task<Product> GetById(int id)
    {
        return await _context.Products.Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Product> Create(Product Product)
    {
        _context.Products.Add(Product);
        await _context.SaveChangesAsync();
        return Product;
    }
    public async Task<Product> Update(Product Product)
    {
        _context.Entry(Product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return Product;
    }

    public async Task<Product> Delete(int id)
    {
        var Product = await GetById(id);
        _context.Products.Remove(Product);
        await _context.SaveChangesAsync();
        return Product;
    }
}
