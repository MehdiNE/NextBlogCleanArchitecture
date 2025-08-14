using Microsoft.EntityFrameworkCore;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Comments;
using NextBlogCleanArchitecture.Infrastructure.Common.Persistence;
using System.Threading;

namespace NextBlogCleanArchitecture.Infrastructure.Comments.Persistence
{
    public class CommentRepository : ICommentRepository
    {
        private readonly NextBlogDbContext _dbContext;

        public CommentRepository(NextBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateAsync(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
            return true;
        }

        public async Task<bool> DeleteByIdAsync(Comment comment)
        {
            _dbContext.Comments.Remove(comment);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Comments.FindAsync(new object[] { id });
        }
    }
}
