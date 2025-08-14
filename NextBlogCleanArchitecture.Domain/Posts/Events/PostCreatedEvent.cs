using NextBlogCleanArchitecture.Domain.Abstractions;

namespace NextBlogCleanArchitecture.Domain.Posts.Events
{
    public record PostCreatedEvent(Guid postId) : IDomainEvent;
}
