namespace NextBlogCleanArchitecture.Domain.Post
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
