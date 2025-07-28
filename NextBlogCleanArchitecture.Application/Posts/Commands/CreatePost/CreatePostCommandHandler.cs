using ErrorOr;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, ErrorOr<Guid>>
    {
        public async Task<ErrorOr<Guid>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            return Guid.NewGuid();
        }
    }
}
