using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.WEB.Models;
using Shop.WEB.Services.Interfaces;
using System.Diagnostics;

namespace Shop.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllProducts(string.Empty);

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProductViewModel>> ProductDetails(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var result = await _productService.GetProductById(id, token);
            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost]
        [ActionName("ProductDetails")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> ProductDetailsPost(ProductViewModel productView)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            CartViewModel cart = new()
            {
                CartHeader = new CartHeaderViewModel
                {
                    UserId = User.Claims.Where(u => u.Type == "sub")?.FirstOrDefault()?.Value
                }
            };

            CartItemViewModel cartItem = new()
            {
                Quantity = productView.Quantity,
                ProductId = productView.Id,
                Product = await _productService.GetProductById(productView.Id, token)
            };

            List<CartItemViewModel> cartItemsVM = new List<CartItemViewModel>();
            cartItemsVM.Add(cartItem);
            cart.CartItems = cartItemsVM;

            var result = await _cartService.AddItemToCartAsync(cart, token);

            if (result is not null)
                return RedirectToAction(nameof(Index));

            return View(productView);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public async Task<IActionResult> Login()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}