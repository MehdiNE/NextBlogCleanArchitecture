using NextBlogCleanArchitecture.Application.Posts.Queries;
using NextBlogCleanArchitecture.Domain.Posts;

namespace NextBlogCleanArchitecture.Application.Abstractions
{
    public interface IPostRepository
    {
        Task CreatePost(Post post);
        Task<Post?> GetByIdAsync(Guid postId);
        IQueryable<Post> GetAllPosts(string? searchTerms, string? sortColumn, string? sortOrder);
        Task UpdatePostAsync(Post post);
    }
}
