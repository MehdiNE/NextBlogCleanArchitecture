using MediatR;
using Microsoft.AspNetCore.Mvc;
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

            if (createPostResult.IsError)
            {
                return Problem();
            }

            return Ok(createPostResult.Value);
        }


        [HttpGet("{postId:guid}")]
        public async Task<IActionResult> GetPostById(Guid postId)
        {
            var query = new GetPostQuery(postId);

            var getPostResult = await _mediator.Send(query);

            if (getPostResult.IsError)
            {
                return Problem(getPostResult.FirstError.Description);
            }

            return Ok(getPostResult.Value);


        }
    }
}
