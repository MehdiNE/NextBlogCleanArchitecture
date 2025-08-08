using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public record GetPostQuery(Guid PostId) : IRequest<Result<PostResponse>>;
}
