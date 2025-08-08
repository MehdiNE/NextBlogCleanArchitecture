using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.CreatePost
{
    public record CreatePostCommand(string Title, string Content)
        : IRequest<Result<Guid>>;
}
