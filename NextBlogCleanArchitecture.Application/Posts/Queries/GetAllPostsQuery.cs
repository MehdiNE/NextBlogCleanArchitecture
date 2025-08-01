using MediatR;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public record GetAllPostsQuery() : IRequest<List<Post>>;
}
