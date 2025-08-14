using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Authentication.DTOs;

namespace NextBlogCleanArchitecture.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<AuthResponse>>
    {
        public required string Username { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
}
