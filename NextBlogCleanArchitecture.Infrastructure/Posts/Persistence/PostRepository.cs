using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Infrastructure.Posts.Persistence
{
    public class PostRepository : IPostRepository
    {
        private readonly List<Post> _posts = [];

        public Task CreatePost(Post post)
        {
            _posts.Add(post);

            return Task.CompletedTask;
        }

        public Task<Post?> GetByIdAsync(Guid postId)
        {
            var post = _posts.SingleOrDefault(post => post.Id == postId);
            return Task.FromResult(post);
        }
    }
}
