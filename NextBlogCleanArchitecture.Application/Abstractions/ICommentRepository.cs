using NextBlogCleanArchitecture.Domain.Comments;

namespace NextBlogCleanArchitecture.Application.Abstractions
{
    public interface ICommentRepository
    {
        Task<bool> CreateAsync(Comment comment);
        Task<Comment?> GetByIdAsync(Guid id);
        Task<bool> DeleteByIdAsync(Comment comment);
    }
}
