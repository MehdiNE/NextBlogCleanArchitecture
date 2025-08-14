using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Comments.Commands.DeleteComment
{
    public record DeleteCommentCommand(Guid PostId, Guid CommentId) : IRequest<Result>;
}
