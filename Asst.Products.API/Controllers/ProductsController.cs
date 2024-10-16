using Asst.Products.API.Models;
using Asst.Products.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asst.Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Product>>>> GetProducts()
        {
            var products = await _service.GetProductsAsync();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Product>>> GetProduct(int id)
        {
            var product = await _service.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(new ApiResponse<Product>(false, "Product not found", null));
            }

            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Product>>> PostProduct(CreateProduct product)
        {
            var createdProduct = await _service.AddProductAsync(product);

            if (!createdProduct.Success)
            {
                return BadRequest(createdProduct);
            }

            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Data.Id }, new ApiResponse<Product>(true, "Product created successfully", createdProduct.Data));
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest(new ApiResponse<Product>(false, "Product ID mismatch", null));
            }

            await _service.UpdateProductAsync(product);
            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
           var res =  await _service.DeleteProductAsync(id);

            if (!res.Data)
            {
                return NotFound(res);
            }

            return NoContent();
        }

        // PUT: api/Products/decrement-stock/5/10
        [HttpPut("decrement-stock/{id}/{quantity}")]
        public async Task<IActionResult> DecrementStock(int id, int quantity)
        {
            var res = await _service.DecrementStockAsync(id, quantity);

            if (!res.Data)
            {
                return BadRequest(res);
            }

            return Ok(new ApiResponse<string>(true, "Stock decremented successfully", null));
        }

        // PUT: api/Products/add-to-stock/5/10
        [HttpPut("add-to-stock/{id}/{quantity}")]
        public async Task<IActionResult> AddToStock(int id, int quantity)
        {
            var res = await _service.AddToStockAsync(id, quantity);

            if (!res.Data)
            {
                return BadRequest(res);
            }

            return Ok(new ApiResponse<string>(true, "Stock added successfully", null));
        }
    }
}
