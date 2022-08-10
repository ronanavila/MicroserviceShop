using Microsoft.AspNetCore.Mvc;
using Shop.ProductApi.DTOs;
using Shop.ProductApi.Services;

namespace Shop.ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdcutsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProdcutsController(IProductService ProductService)
        {
            _productService = ProductService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            var ProductsDTO = await _productService.GetProducts();
            if (ProductsDTO is null)
                return NotFound("Product Not Found");

            return Ok(ProductsDTO);
        }
       

        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            var ProductsDTO = await _productService.GetProductByID(id);
            if (ProductsDTO is null)
                return NotFound("Product Not Found");

            return Ok(ProductsDTO);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Post([FromBody] ProductDTO ProductDTO)
        {
            if (ProductDTO is null)
                return BadRequest("Invalid Data");

            await _productService.AddProduct(ProductDTO);


            return new CreatedAtRouteResult("GetProduct", new { id = ProductDTO.Id }, ProductDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Put(int id, [FromBody] ProductDTO ProductDTO)
        {
            if (id != ProductDTO.Id)
                return BadRequest();

            if (ProductDTO is null)
                return BadRequest();

            await _productService.UpdateProduct(ProductDTO);

            return Ok(ProductDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var ProductDTO = await _productService.GetProductByID(id);

            if (ProductDTO is null)
                return NotFound("Product Not Found");

            await _productService.RemoveProduct(id);

            return Ok(ProductDTO);
        }
    }
}
