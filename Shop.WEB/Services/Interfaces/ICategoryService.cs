using Shop.WEB.Models;

namespace Shop.WEB.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategories(string token);
}
