using Asst.Products.API.Models;
using Asst.Products.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asst.Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly ISellerService _service;

        public SellersController(ISellerService service)
        {
            _service = service;
        }

        // GET: api/Sellers
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<Seller>>>> GetSellers()
        {
            var sellers = await _service.GetSellersAsync();
            return Ok(new ApiResponse<IEnumerable<Seller>>(true, "Sellers retrieved successfully", sellers));
        }

        // GET: api/Sellers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Seller>>> GetSeller(int id)
        {
            var seller = await _service.GetSellerByIdAsync(id);

            if (seller == null)
            {
                return NotFound(new ApiResponse<Seller>(false, "Seller not found", null));
            }

            return Ok(new ApiResponse<Seller>(true, "Seller retrieved successfully", seller));
        }

        // POST: api/Sellers
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Seller>>> PostSeller(CreateSeller createSeller)
        {
            var createdSeller = await _service.AddSellerAsync(createSeller);
            return CreatedAtAction(nameof(GetSeller), new { id = createdSeller.Id }, new ApiResponse<Seller>(true, "Seller created successfully", createdSeller));
        }

        // PUT: api/Sellers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest(new ApiResponse<Seller>(false, "Seller ID mismatch", null));
            }

            await _service.UpdateSellerAsync(seller);
            return NoContent();
        }
    }
}
