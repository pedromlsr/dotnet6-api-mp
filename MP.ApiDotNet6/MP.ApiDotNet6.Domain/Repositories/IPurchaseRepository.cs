using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Domain.Repositories
{
    public interface IPurchaseRepository
    {
        Task<ICollection<Purchase>> GetAllAsync();
        Task<Purchase> GetByIdAsync(int id);
        //Task<ICollection<Purchase>> GetByPersonIdAsync(int personId);
        //Task<ICollection<Purchase>> GetByProductIdAsync(int productId);
        Task<Purchase> CreateAsync(Purchase Purchase);
        Task EditAsync(Purchase Purchase);
        Task DeleteAsync(Purchase Purchase);
    }
}
