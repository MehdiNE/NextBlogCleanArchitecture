using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Follows.Queries.GetFollowings
{
    public class GetFollowingsQueryHandler : IRequestHandler<GetFollowingsQuery, Result<List<Guid>>>
    {
        private readonly IUserRepository _userRepository;

        public GetFollowingsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<Guid>>> Handle(GetFollowingsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null) return Result.Fail(UserErrors.NotFound);

            var followings = await _userRepository.GetFollowingsAsync(request.UserId);
            var followingIds = followings.Select(f => f.Id).ToList();

            return Result.Ok(followingIds);
        }
    }
}
