using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Comments.Commands.CreateComment
{
    public record CreateCommentCommand(Guid PostId, string Content) : IRequest<Result>;
}
