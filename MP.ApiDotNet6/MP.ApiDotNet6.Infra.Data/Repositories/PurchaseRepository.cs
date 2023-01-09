using Microsoft.EntityFrameworkCore;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Context;

namespace MP.ApiDotNet6.Infra.Data.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _db;

        public PurchaseRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<Purchase>> GetAllAsync()
        {
            return await _db.Purchases
                .Include(x => x.Person)
                .Include(x => x.Product)
                .ToListAsync();
        }

        public async Task<Purchase> GetByIdAsync(int id)
        {
            return await _db.Purchases
                .Include(x => x.Person)
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        //public async Task<ICollection<Purchase>> GetByPersonIdAsync(int personId)
        //{
        //    return await _db.Purchases
        //        .Include(x => x.Person)
        //        .Include(x => x.Product)
        //        .Where(x => x.PersonId == personId);
        //}

        //public async Task<ICollection<Purchase>> GetByProductIdAsync(int productId)
        //{
        //    return await _db.Purchases
        //        .Include(x => x.Person)
        //        .Include(x => x.Product)
        //        .Where(x => x.ProductId == productId);
        //}

        public async Task<Purchase> CreateAsync(Purchase purchase)
        {
            _db.Add(purchase);
            await _db.SaveChangesAsync();
            return purchase;
        }

        public async Task EditAsync(Purchase purchase)
        {
            _db.Update(purchase);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Purchase purchase)
        {
            _db.Remove(purchase);
            await _db.SaveChangesAsync();
        }
    }
}