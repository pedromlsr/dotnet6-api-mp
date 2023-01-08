using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<int> GetIdByCodErpAsync(string codErp);
        Task<Product> CreateAsync(Product Product);
        Task EditAsync(Product Product);
        Task DeleteAsync(Product Product);
    }
}
