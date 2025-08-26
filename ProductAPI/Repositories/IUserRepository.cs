using ProductAPI.Models;

namespace ProductAPI.Repositories
{
    /// Repository only talks to the database (no business rules here).
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<bool> UsernameExistsAsync(string username);
        Task<User> AddAsync(User user);
    }
}
