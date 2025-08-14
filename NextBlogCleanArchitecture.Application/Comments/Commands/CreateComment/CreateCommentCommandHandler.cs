using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Comments;
using NextBlogCleanArchitecture.Domain.Posts;

namespace NextBlogCleanArchitecture.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result>
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommentCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
        }

        public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);

            if (post is null)
            {
                return Result.Fail(PostErrors.NotFound);
            }

            var comment = new Comment(post.Id, request.Content);

            var isCreated = await _commentRepository.CreateAsync(comment);

            if (!isCreated)
            {
                return Result.Fail("Failed to create comment");
            }

            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return Result.Ok();
        }
    }
}
