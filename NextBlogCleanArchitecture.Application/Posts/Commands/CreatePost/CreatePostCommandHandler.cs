using ErrorOr;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<Guid>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;


        public CreatePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = Post.Publish(new Guid(), request.title, request.content);

            await _postRepository.CreatePost(post);
            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return post.Id;
        }
    }
}
