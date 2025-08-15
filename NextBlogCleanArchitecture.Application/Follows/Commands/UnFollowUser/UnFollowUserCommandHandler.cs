using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Follows.Commands.UnFollowUser
{
    public class UnFollowUserCommandHandler : IRequestHandler<UnFollowUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnFollowUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UnFollowUserCommand request, CancellationToken cancellationToken)
        {
            var followerUser = await _userRepository.GetByIdAsync(request.FollowerId);
            if (followerUser is null) return Result.Fail(UserErrors.NotFound);

            var followingUser = await _userRepository.GetByIdAsync(request.FollowingId);
            if (followingUser is null) return Result.Fail(UserErrors.NotFound);

            var unFollowResult = followerUser.Unfollow(followingUser);
            if (unFollowResult.IsFailed) return unFollowResult;

            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
