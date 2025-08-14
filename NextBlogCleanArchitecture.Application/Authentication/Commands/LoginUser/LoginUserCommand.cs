using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Authentication.DTOs;

namespace NextBlogCleanArchitecture.Application.Authentication.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Result<AuthResponse>>
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
