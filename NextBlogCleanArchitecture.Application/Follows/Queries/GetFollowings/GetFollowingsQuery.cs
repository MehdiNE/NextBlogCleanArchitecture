using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Follows.Queries.GetFollowings
{
    public record GetFollowingsQuery(Guid UserId) : IRequest<Result<List<Guid>>>;
}
