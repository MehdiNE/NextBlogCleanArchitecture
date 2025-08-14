using Microsoft.EntityFrameworkCore;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Posts;
using NextBlogCleanArchitecture.Infrastructure.Common.Persistence;
using System.Linq.Expressions;

namespace NextBlogCleanArchitecture.Infrastructure.Posts.Persistence
{
    public class PostRepository : IPostRepository
    {
        private readonly NextBlogDbContext _dbContext;

        public PostRepository(NextBlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePost(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
        }

        public IQueryable<Post> GetAllPosts(string? searchTerms, string? sortColumn, string? sortOrder)
        {
            var query = _dbContext.Posts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerms))
            {
                query = query.Where(x => x.Title.Contains(searchTerms) ||
                x.Content.Contains(searchTerms));
            }

            Expression<Func<Post, object>> keySelector = sortColumn?.ToLower() switch
            {
                "title" => post => post.Title,
                _ => post => post.Id,
            };

            if (sortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(keySelector);
            }
            else
            {
                query = query.OrderBy(keySelector);
            }

            return query;
        }

        public async Task<Post?> GetByIdAsync(Guid postId)
        {
            var post = await _dbContext.Posts.Include(p => p.Comments).SingleOrDefaultAsync(post => post.Id == postId);
            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }
    }
}
