using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Abstractions
{
    public interface IPostRepository
    {
        Task CreatePost(Post post);
        Task<Post?> GetByIdAsync(Guid postId);
        Task<List<Post>> GetAllPosts();
        Task UpdatePostAsync(Post post);
    }
}
