using FluentResults;
using MediatR;

namespace NextBlogCleanArchitecture.Application.Posts.Commands.ArchivePost
{
    public record ArchivePostCommand(Guid PostId) : IRequest<Result>;
}
