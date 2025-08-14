using MediatR;
using Microsoft.AspNetCore.Mvc;
using NextBlogCleanArchitecture.Application.Authentication.Commands.LoginUser;
using NextBlogCleanArchitecture.Application.Authentication.Commands.RegisterUser;
using NextBlogCleanArchitecture.Application.Authentication.DTOs;

namespace NextBlogCleanArchitecture.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISender _mediator;

        public AuthController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };

            var result = await _mediator.Send(command);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var command = new LoginUserCommand
            {
                Email = request.Email,
                Password = request.Password
            };
            var result = await _mediator.Send(command);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
    }
}
