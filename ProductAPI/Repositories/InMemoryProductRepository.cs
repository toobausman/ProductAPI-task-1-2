using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public InMemoryProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
            => await _db.Products.AsNoTracking().ToListAsync();

        public async Task<Product?> GetByIdAsync(int id)
            => await _db.Products.FindAsync(id);

        public async Task<Product> AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            if (!await _db.Products.AnyAsync(p => p.Id == product.Id)) return false;
            _db.Entry(product).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _db.Products.FindAsync(id);
            if (entity is null) return false;
            _db.Products.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SaveChangesAsync()
            => (await _db.SaveChangesAsync()) > 0;
    }
}
