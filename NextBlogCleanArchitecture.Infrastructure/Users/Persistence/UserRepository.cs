using Microsoft.EntityFrameworkCore;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Users;
using NextBlogCleanArchitecture.Infrastructure.Common.Persistence;

namespace NextBlogCleanArchitecture.Infrastructure.Users.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly NextBlogDbContext _dbContext;

        public UserRepository(NextBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.DomainUsers.AddAsync(user);
        }

        public async Task<bool> ExistsAsync(string email)
        {
            return await _dbContext.DomainUsers.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _dbContext.DomainUsers.SingleOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            var user = await _dbContext.DomainUsers.SingleOrDefaultAsync(x => x.Id == id);
            return user;
        }

        public void UpdateAsync(User user)
        {
            _dbContext.DomainUsers.Update(user);
        }
    }
}
