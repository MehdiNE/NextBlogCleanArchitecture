using FluentResults;
using MediatR;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Follows.Queries.GetFollowers
{
    public class GetFollowersQueryHandler : IRequestHandler<GetFollowersQuery, Result<List<Guid>>>
    {
        private readonly IUserRepository _userRepository;

        public GetFollowersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<List<Guid>>> Handle(GetFollowersQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.userId);
            if (user is null) return Result.Fail(UserErrors.NotFound);

            var followers = await _userRepository.GetFollowersAsync(request.userId);
            var followerIds = followers.Select(f => f.Id).ToList();

            return Result.Ok(followerIds);
        }
    }
}
