using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Posts;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.ArchivePost
{
    public class ArchivePostCommandHandler : IRequestHandler<ArchivePostCommand, Result>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchivePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(ArchivePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.PostId);

            if (post is null)
            {
                return Result.Fail(PostErrors.NotFound);
            }

            var archiveResult = post.Archive();

            if (archiveResult.IsFailed)
            {
                return Result.Fail(archiveResult.Errors);
            }

            await _postRepository.UpdatePostAsync(post);
            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return archiveResult;
        }
    }
}
