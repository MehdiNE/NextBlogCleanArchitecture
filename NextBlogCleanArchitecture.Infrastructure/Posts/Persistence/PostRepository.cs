using Microsoft.EntityFrameworkCore;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Post;
using NextBlogCleanArchitecture.Infrastructure.Common.Persistence;

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

        public async Task<List<Post>> GetAllPosts()
        {
            var posts = await _dbContext.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post?> GetByIdAsync(Guid postId)
        {
            var post = await _dbContext.Posts.SingleOrDefaultAsync(post => post.Id == postId);
            return post;
        }

        public Task UpdatePostAsync(Post post)
        {
            _dbContext.Update(post);

            return Task.CompletedTask;
        }
    }
}
