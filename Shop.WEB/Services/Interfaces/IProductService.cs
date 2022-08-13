using Shop.WEB.Models;

namespace Shop.WEB.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProducts();
    Task<ProductViewModel> GetProductById(int id);
    Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel);
    Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel);
    Task<bool> DeleteProductById(int id);
}
