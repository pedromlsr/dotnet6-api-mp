using MP.ApiDotNet6.Application.DTOs;

namespace MP.ApiDotNet6.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<ResultService<ICollection<ProductDTO>>> GetByIdAsync();
        Task<ResultService<ProductDTO>> GetByIdAsync(int id);
        Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO);
        Task<ResultService> EditAsync(ProductDTO productDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
