using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Follows.Commands.UnFollowUser
{
    public record UnFollowUserCommand(Guid FollowerId, Guid FollowingId) : IRequest<Result>;
}
