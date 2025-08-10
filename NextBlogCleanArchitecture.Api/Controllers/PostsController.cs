using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextBlogCleanArchitecture.Application.Posts.Commands.ArchivePost;
using NextBlogCleanArchitecture.Application.Posts.Commands.CreatePost;
using NextBlogCleanArchitecture.Application.Posts.Queries;
using NextBlogCleanArchitecture.Contracts.Post;

namespace NextBlogCleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ISender _mediator;

        public PostsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest request)
        {
            var command = new CreatePostCommand(request.Title, request.Content);

            var createPostResult = await _mediator.Send(command);

            if (createPostResult.IsFailed)
            {
                return BadRequest(createPostResult.Errors.Select(e => new { e.Message, e.Metadata }));
            }

            return Ok(createPostResult.Value);
        }


        [HttpGet("{postId:guid}")]
        public async Task<IActionResult> GetPostById(Guid postId)
        {
            var query = new GetPostQuery(postId);

            var getPostResult = await _mediator.Send(query);

            if (getPostResult.IsFailed)
            {
                return BadRequest(getPostResult.Errors.Select(e => new { e.Message, e.Metadata }));
            }

            return Ok(getPostResult.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllPostQueryParams queryParams)
        {
            var query = new GetAllPostsQuery(queryParams);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("archive/{postId}")]
        public async Task<IActionResult> Archive(Guid postId)
        {
            var command = new ArchivePostCommand(postId);

            var archiveResult = await _mediator.Send(command);

            if (archiveResult.IsFailed)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
