using Microsoft.AspNetCore.Mvc;
using WebApplication13.DTO;
using WebApplication13.Services;

namespace WebApplication13.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var (items, totalCount) = await _service.GetAllAsync(page, pageSize, ct);

            // Добавляем заголовок X-Total-Count
            Response.Headers.Append("X-Total-Count", totalCount.ToString());

            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var product = await _service.GetByIdAsync(id, ct);
            return product is null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO dto, CancellationToken ct)
        {
            var product = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = product.IdProduct }, product);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateProductDTO dto, CancellationToken ct)
        {
            var product = await _service.UpdateAsync(id, dto, ct);
            return product is null ? NotFound() : NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id, PatchProductDTO dto, CancellationToken ct)
        {
            var product = await _service.PatchAsync(id, dto, ct);
            return product is null ? NotFound() : NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var deleted = await _service.DeleteAsync(id, ct);
            return deleted ? NoContent() : NotFound();
        }

        // ================= БИЗНЕС-ОПЕРАЦИИ =================

        [HttpPost("place-order")]
        public async Task<IActionResult> PlaceOrder(CreateOrderDto dto, CancellationToken ct)
        {
            try
            {
                var order = await _service.PlaceOrderAsync(dto, ct);
                return Ok(order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("cancel-order/{orderId:int}")]
        public async Task<IActionResult> CancelOrder(int orderId, CancellationToken ct)
        {
            var order = await _service.CancelOrderAsync(orderId, ct);
            return order is null ? NotFound() : Ok(order);
        }
    }
}