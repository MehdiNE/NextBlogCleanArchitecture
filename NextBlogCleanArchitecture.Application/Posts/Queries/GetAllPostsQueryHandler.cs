using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, PostsResponse>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostsResponse> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetAllPosts();

            var response = new PostsResponse
            {
                Items = posts.Select(x => new PostResponse
                {
                    AuthorName = string.Empty,
                    Content = x.Content,
                    Title = x.Title,
                    PostStatus = x.PostStatus,
                    Id = x.Id,
                    CreatedAt = x.PublishedAt
                })
            };

            return response;
        }
    }
}
