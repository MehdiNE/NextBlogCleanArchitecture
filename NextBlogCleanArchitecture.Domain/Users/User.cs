using FluentResults;
using NextBlogCleanArchitecture.Domain.Abstractions;
using NextBlogCleanArchitecture.Domain.Follows;
using NextBlogCleanArchitecture.Domain.Users.Events;

namespace NextBlogCleanArchitecture.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id) : base(id) { }

        public string Username { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public DateTime CreatedAt { get; private init; }
        public ICollection<Follow> Followings { get; private set; } = [];
        public ICollection<Follow> Followers { get; private set; } = [];



        public static User Create(Guid id, string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new ArgumentException("Invalid email.");

            var user = new User(id)
            {
                Username = username,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            user.RaiseDomainEvents(new UserCreatedDomainEvent(user.Id));

            return user;
        }

        public Result FollowUser(User followedUser)
        {
            if (Id == followedUser.Id)
                return Result.Fail(FollowErrors.CantFollowYourSelf);

            if (Followings.Any(f => f.FollowingId == followedUser.Id))
                return Result.Fail(FollowErrors.AlreadyFollowing);

            Followings.Add(Follow.Create(Id, followedUser.Id).Value);
            return Result.Ok();
        }

        public Result Unfollow(User followedUser)
        {
            var follow = Followings.FirstOrDefault(f => f.FollowingId == followedUser.Id);
            if (follow == null)
                return Result.Fail(FollowErrors.NotFollowing);

            Followings.Remove(follow);
            return Result.Ok();
        }
    }
}
