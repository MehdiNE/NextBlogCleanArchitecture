using FluentResults;
using NextBlogCleanArchitecture.Domain.Abstractions;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Domain.Follows
{
    public class Follow : Entity
    {
        private Follow() { }
        private Follow(Guid followerId, Guid followingId, DateTime createdAt) : base()
        {
            FollowerId = followerId;
            FollowingId = followingId;
            CreatedAt = createdAt;
        }

        public Guid FollowerId { get; private set; }
        public Guid FollowingId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public User Follower { get; private set; }
        public User Following { get; private set; }

        public static Result<Follow> Create(Guid followerId, Guid followingId)
        {
            if (followerId == followingId)
            {
                return Result.Fail(FollowErrors.CantFollowYourSelf);
            }

            return new Follow(followerId, followingId, DateTime.UtcNow);
        }
    }
}
