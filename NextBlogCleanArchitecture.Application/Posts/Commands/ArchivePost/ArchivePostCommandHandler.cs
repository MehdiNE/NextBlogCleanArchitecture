using Bookify.Domain.Bookings;
using ErrorOr;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Post;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.ArchivePost
{
    public class ArchivePostCommandHandler : IRequestHandler<ArchivePostCommand, ErrorOr<Success>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ArchivePostCommandHandler(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Success>> Handle(ArchivePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetByIdAsync(request.postId);

            if (post is null)
            {
                return PostErrors.NotFound;
            }

            var archiveResult = post.Archive();

            if (archiveResult.IsError)
            {
                return archiveResult.FirstError;
            }

            await _postRepository.UpdatePostAsync(post);
            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return archiveResult;
        }
    }
}
