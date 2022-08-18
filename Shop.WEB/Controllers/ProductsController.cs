using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.WEB.Models;
using Shop.WEB.Roles;
using Shop.WEB.Services.Interfaces;


namespace Shop.WEB.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    public ProductsController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductViewModel>>> Index()
    {
        var result = await _productService.GetAllProducts(await GetAccessToken());

        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(await GetAccessToken()), "Id", "Name");
        return View();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProduct(ProductViewModel productViewModel)
    {
        var token = await GetAccessToken();

        if (ModelState.IsValid)
        {
            var result = await _productService.CreateProduct(productViewModel, token);
            if (result is not null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(token), "Id", "Name");
        }

        return View(productViewModel);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateProduct(int id)
    {
        var token = await GetAccessToken();

        ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(token), "Id", "Name");
        var result = await _productService.GetProductById(id, token);
        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> UpdateProduct(ProductViewModel productViewModel)
    {
        var token = await GetAccessToken();
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(productViewModel, token);
            if (result is not null)
                return RedirectToAction(nameof(Index));
        }
        else
        {
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllCategories(token), "Id", "Name");
        }

        return View(productViewModel);
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<ProductViewModel>> DeleteProduct(int id)
    {
        var result = await _productService.GetProductById(id, await GetAccessToken());
        if (result is null)
            return View("Error");

        return View(result);
    }

    [HttpPost(), ActionName("DeleteProduct")]
    [Authorize(Roles = Role.Admin)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await _productService.DeleteProductById(id, await GetAccessToken());

        if (!result)
            return View("Error");

        return RedirectToAction("Index");
    }

    private async Task<string> GetAccessToken()
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        return token;
    }
}
