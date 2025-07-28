using ErrorOr;
using MediatR;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public record GetPostQuery(Guid PostId) : IRequest<ErrorOr<Post>>;
}
