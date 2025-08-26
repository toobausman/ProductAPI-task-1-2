using ProductAPI.DTOs;
using ProductAPI.Results;

namespace ProductAPI.Services
{
    // Services decide outcomes; controllers just return them.
    public interface IProductService
    {
        Task<ApiResult<IEnumerable<ProductReadDto>>> GetAllAsync();
        Task<ApiResult<ProductReadDto>> GetByIdAsync(int id);
        Task<ApiResult<ProductReadDto>> CreateAsync(ProductCreateDto dto);
        Task<ApiResult<object>> UpdateAsync(int id, ProductUpdateDto dto);
        Task<ApiResult<object>> DeleteAsync(int id);
    }
}
