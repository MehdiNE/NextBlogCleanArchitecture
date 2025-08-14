using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Application.Authentication.DTOs;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Authentication.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IIdentityService identityService, IUserRepository userRepository)
        {
            _identityService = identityService;
            _userRepository = userRepository;
        }

        public async Task<Result<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user is null)
            {
                return Result.Fail(UserErrors.NotFound);
            }

            var succeeded = await _identityService.LoginAsync(request.Email, request.Password);
            if (!succeeded)
            {
                return Result.Fail(UserErrors.InvalidCredentials);
            }

            var token = await _identityService.GenerateTokenAsync(user);
            var authResponse = new AuthResponse
            {
                Token = token
            };
            return Result.Ok(authResponse);
        }
    }
}
