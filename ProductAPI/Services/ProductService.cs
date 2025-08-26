using ProductAPI.DTOs;
using ProductAPI.Models;
using ProductAPI.Repositories;
using ProductAPI.Results;

namespace ProductAPI.Services
{
    /// <summary>
    /// All business rules + mapping here.
    /// Returns ApiResult so controllers never branch.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<ApiResult<IEnumerable<ProductReadDto>>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            var data = products.Select(MapToReadDto);
            return ApiResult<IEnumerable<ProductReadDto>>.Ok(data);
        }

        public async Task<ApiResult<ProductReadDto>> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return ApiResult<ProductReadDto>.NotFound();
            return ApiResult<ProductReadDto>.Ok(MapToReadDto(entity));
        }

        public async Task<ApiResult<ProductReadDto>> CreateAsync(ProductCreateDto dto)
        {
            var entity = new Product
            {
                Name = dto.Name.Trim(),
                Price = dto.Price,
                Quantity = dto.Quantity
            };

            var saved = await _repo.AddAsync(entity);
            var result = MapToReadDto(saved);

            // include route info so controller can emit 201 Created with Location header
            return ApiResult<ProductReadDto>.Created(
                result,
                routeName: "GetProductById",
                routeValues: new { id = result.Id }
            );
        }

        public async Task<ApiResult<object>> UpdateAsync(int id, ProductUpdateDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return ApiResult<object>.NotFound();

            entity.Name = dto.Name.Trim();
            entity.Price = dto.Price;
            entity.Quantity = dto.Quantity;

            var ok = await _repo.UpdateAsync(entity);
            return ok ? ApiResult<object>.NoContent()
                      : ApiResult<object>.NotFound();
        }

        public async Task<ApiResult<object>> DeleteAsync(int id)
        {
            var ok = await _repo.DeleteAsync(id);
            return ok ? ApiResult<object>.NoContent()
                      : ApiResult<object>.NotFound();
        }

        // ---- mapping helpers (internal to service) ----
        private static ProductReadDto MapToReadDto(Product p) => new()
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Quantity = p.Quantity
        };
    }
}
