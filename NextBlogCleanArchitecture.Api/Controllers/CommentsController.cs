using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NextBlogCleanArchitecture.Application.Comments.Commands.CreateComment;
using NextBlogCleanArchitecture.Application.Comments.Commands.DeleteComment;

namespace NextBlogCleanArchitecture.Api.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ISender _mediator;

        public CommentsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{postId:guid}/comments")]
        public async Task<IActionResult> CreateComment(Guid postId, [FromBody] CreateCommentRequest request)
        {
            var command = new CreateCommentCommand(postId, request.Content);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return NotFound(result);
            }

            return Ok();
        }

        [HttpDelete("{postId:guid}/comments/{commentId:guid}")]
        public async Task<IActionResult> Delete(Guid postId, Guid commentId)
        {
            var command = new DeleteCommentCommand(postId, commentId);

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
