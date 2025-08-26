using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;

namespace ProductAPI.Repositories
{
  
    /// EF Core implementation using the same InMemory database.
    
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) => _db = db;

        public async Task<User?> GetByUsernameAsync(string username)
            => await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);

        public async Task<bool> UsernameExistsAsync(string username)
            => await _db.Users.AnyAsync(u => u.Username == username);

        public async Task<User> AddAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}
