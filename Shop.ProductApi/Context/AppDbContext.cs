using Microsoft.EntityFrameworkCore;
using Shop.ProductApi.Models;

namespace Shop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
      

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Category>()
            .HasMany(g => g.Products).WithOne(c => c.Category).IsRequired().OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>().HasKey(c => c.Id);
        modelBuilder.Entity<Product>().Property(c => c.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Product>().Property(c => c.Description).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Product>().Property(c => c.ImageUrl).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Product>().Property(c => c.Price).HasPrecision(16,2);


        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Eraser",
            },
            new Category
            {
                Id = 2,
                Name = "Pencil"
            });

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .LogTo(x => Console.WriteLine(x))
            .EnableSensitiveDataLogging();

        base.OnConfiguring(optionsBuilder);
    }
}
