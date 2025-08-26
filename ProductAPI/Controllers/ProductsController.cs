using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.DTOs;
using ProductAPI.Results;
using ProductAPI.Services;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // all endpoints require JWT (per your requirement) :contentReference[oaicite:0]{index=0}
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductsController(IProductService service) => _service = service;

        // GET /api/products
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => (await _service.GetAllAsync()).ToActionResult(this);

        // GET /api/products/{id}
        [HttpGet("{id:int}", Name = "GetProductById")]
        public async Task<IActionResult> GetById(int id)
            => (await _service.GetByIdAsync(id)).ToActionResult(this);

        // POST /api/products
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto)
            => (await _service.CreateAsync(dto)).ToActionResult(this);

        // PUT /api/products/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ProductUpdateDto dto)
            => (await _service.UpdateAsync(id, dto)).ToActionResult(this);

        // DELETE /api/products/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
            => (await _service.DeleteAsync(id)).ToActionResult(this);
    }
}
