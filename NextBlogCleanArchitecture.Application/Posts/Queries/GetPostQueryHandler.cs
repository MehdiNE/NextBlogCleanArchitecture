using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, Result<PostResponse>>
    {
        private readonly IPostRepository _postRepository;

        public GetPostQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Result<PostResponse>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);
            if (post is null)
            {
                return Result.Fail(PostErrors.NotFound);
            }

            var response = new PostResponse
            {
                AuthorName = string.Empty,
                Content = post.Content,
                Title = post.Title,
                PostStatus = post.PostStatus,
                Id = post.Id,
                CreatedAt = post.PublishedAt
            };

            return Result.Ok(response);

        }
    }
}