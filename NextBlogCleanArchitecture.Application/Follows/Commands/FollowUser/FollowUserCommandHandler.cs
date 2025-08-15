using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Follows.Commands.FollowUser
{
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand, Result>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FollowUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            var followerUser = await _userRepository.GetByIdAsync(request.FollowerId);
            if (followerUser is null) return Result.Fail(UserErrors.NotFound);

            var followingUser = await _userRepository.GetByIdAsync(request.FollowingId);
            if (followingUser is null) return Result.Fail(UserErrors.NotFound);

            var followResult = followerUser.FollowUser(followingUser);
            if (followResult.IsFailed) return followResult;

            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return Result.Ok();
        }
    }
}
