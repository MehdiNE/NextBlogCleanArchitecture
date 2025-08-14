using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        void UpdateAsync(User user);
        Task<bool> ExistsAsync(string email);
    }
}
