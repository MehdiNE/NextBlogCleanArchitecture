using NextBlogCleanArchitecture.Domain.Abstractions;

namespace NextBlogCleanArchitecture.Domain.Post.Events
{
    public record PostCreatedEvent(Guid postId) : IDomainEvent;
}
