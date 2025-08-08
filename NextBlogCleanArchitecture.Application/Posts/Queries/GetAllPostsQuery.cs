using MediatR;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public record GetAllPostsQuery() : IRequest<PostsResponse>;
}
