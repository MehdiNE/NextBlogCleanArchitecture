namespace NextBlogCleanArchitecture.Application.Abstractions
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}
