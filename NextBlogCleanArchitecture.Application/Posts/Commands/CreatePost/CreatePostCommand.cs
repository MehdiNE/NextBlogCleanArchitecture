using ErrorOr;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(string title, string content)
        : IRequest<ErrorOr<Guid>>;
}
