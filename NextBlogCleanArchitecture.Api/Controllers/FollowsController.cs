using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextBlogCleanArchitecture.Api.Extensions;
using NextBlogCleanArchitecture.Application.Follows.Commands.FollowUser;
using NextBlogCleanArchitecture.Application.Follows.Commands.UnFollowUser;
using NextBlogCleanArchitecture.Application.Follows.Queries.GetFollowers;
using NextBlogCleanArchitecture.Application.Follows.Queries.GetFollowings;

namespace NextBlogCleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FollowsController : ControllerBase
    {
        private readonly ISender _mediator;

        public FollowsController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{followingUserId:guid}")]
        public async Task<IActionResult> FollowUser([FromRoute] Guid followingUserId)
        {
            Guid followerId = User.GetUserId();

            var command = new FollowUserCommand(followerId, followingUserId);

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpDelete("{followingUserId:guid}")]
        public async Task<IActionResult> UnFollowUser([FromRoute] Guid followingUserId)
        {
            Guid followerId = User.GetUserId();

            var command = new UnFollowUserCommand(followerId, followingUserId);

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpGet("followers/{userId:guid}")]
        public async Task<IActionResult> GetFollowers([FromRoute] Guid userId)
        {
            var query = new GetFollowersQuery(userId);
            var result = await _mediator.Send(query);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }

        [HttpGet("followings/{userId:guid}")]
        public async Task<IActionResult> GetFollowings([FromRoute] Guid userId)
        {
            var query = new GetFollowingsQuery(userId);
            var result = await _mediator.Send(query);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
    }
}
