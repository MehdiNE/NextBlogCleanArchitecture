using NextBlogCleanArchitecture.Domain.Abstractions;

namespace NextBlogCleanArchitecture.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
}
