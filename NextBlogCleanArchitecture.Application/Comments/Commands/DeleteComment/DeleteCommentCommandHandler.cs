using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Comments;
using NextBlogCleanArchitecture.Domain.Posts;

namespace NextBlogCleanArchitecture.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
    {
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCommentCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _commentRepository = commentRepository;
        }

        public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);

            if (post is null)
            {
                return Result.Fail(PostErrors.NotFound);
            }

            var comment = await _commentRepository.GetByIdAsync(request.CommentId);

            if (comment is null)
            {
                return Result.Fail(CommentErrors.NotFound);
            }

            var isDeleted = await _commentRepository.DeleteByIdAsync(comment);

            if (!isDeleted)
            {
                return Result.Fail("Failed to deleting the post");
            }

            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
