using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Follows.Queries.GetFollowers
{
    public record GetFollowersQuery(Guid userId) : IRequest<Result<List<Guid>>>;
}
