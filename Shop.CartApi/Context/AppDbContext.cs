using Microsoft.EntityFrameworkCore;
using Shop.CartApi.Models;

namespace Shop.CartApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product>? Products { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Product
        mb.Entity<Product>().HasKey(p => p.Id);
        mb.Entity<Product>().Property(p => p.Id).ValueGeneratedNever();
        mb.Entity<Product>().Property(p => p.Name).HasMaxLength(100).IsRequired();
        mb.Entity<Product>().Property(p => p.Description).HasMaxLength(255).IsRequired();
        mb.Entity<Product>().Property(p => p.ImageUrl).HasMaxLength(255).IsRequired();
        mb.Entity<Product>().Property(p => p.CategoryName).HasMaxLength(100).IsRequired();
        mb.Entity<Product>().Property(p => p.Price).HasPrecision(16,2);

        //CartHeader
        mb.Entity<CartHeader>().Property(c => c.UserId).HasMaxLength(255).IsRequired();
        mb.Entity<CartHeader>().Property(c => c.CouponCode).HasMaxLength(100);

        //CartItem
      

    }
}
