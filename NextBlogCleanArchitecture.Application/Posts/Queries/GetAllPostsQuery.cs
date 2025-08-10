using MediatR;
using NextBlogCleanArchitecture.Application.Common;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public record GetAllPostsQuery(GetAllPostQueryParams QueryParams) : IRequest<PagedList<PostResponse>>;
}
