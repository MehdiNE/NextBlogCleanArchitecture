using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Application.Authentication.DTOs;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IIdentityService identityService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _identityService = identityService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<AuthResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var isUserExist = await _userRepository.ExistsAsync(request.Email);
            if (isUserExist)
            {
                return Result.Fail(UserErrors.UserExist);
            }

            var identityUser = new AppUser
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                UserName = request.Username
            };

            var identityResult = await _identityService.RegisterAsync(identityUser, request.Password);
            if (!identityResult.Succeeded)
            {
                return Result.Fail(string.Join(", ", identityResult.Errors.Select(x => x.Code)));
            }


            var user = User.Create(identityUser.Id, request.Username, request.Email);
            await _userRepository.AddAsync(user);

            var token = await _identityService.GenerateTokenAsync(user);

            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return Result.Ok(new AuthResponse
            {
                Token = token
            });

        }
    }
}
