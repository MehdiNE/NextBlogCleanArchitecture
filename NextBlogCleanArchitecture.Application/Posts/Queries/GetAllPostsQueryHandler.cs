using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Application.Common;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PagedList<PostResponse>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PagedList<PostResponse>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var postsQuery = _postRepository.GetAllPosts(request.QueryParams.SearchTerms, request.QueryParams.SortColumn, request.QueryParams.SortOrder);

            var mappedPostsQuery = postsQuery.Select(p => new PostResponse
            {
                AuthorName = "",
                Content = p.Content,
                Title = p.Title,
                CreatedAt = p.PublishedAt,
                Id = p.Id,
                PostStatus = p.PostStatus,
            });

            var posts = await PagedList<PostResponse>.CreateAsync(mappedPostsQuery, request.QueryParams.Page, request.QueryParams.PageSize);

            return posts;
        }
    }
}
