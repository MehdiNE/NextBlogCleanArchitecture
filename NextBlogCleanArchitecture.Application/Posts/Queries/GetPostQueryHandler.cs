using ErrorOr;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, ErrorOr<Post>>
    {
        private readonly IPostRepository _postRepository;

        public GetPostQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<Post>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);
            if (post is null)
            {
                return Error.NotFound(description: "Post not found");
            }

            return post;

        }
    }
}