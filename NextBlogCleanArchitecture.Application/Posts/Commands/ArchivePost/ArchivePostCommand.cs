using ErrorOr;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.ArchivePost
{
    public record ArchivePostCommand(Guid postId) : IRequest<ErrorOr<Success>>;
}
