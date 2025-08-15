using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Follows.Commands.FollowUser
{
    public record FollowUserCommand(Guid FollowerId, Guid FollowingId) : IRequest<Result>;
}
