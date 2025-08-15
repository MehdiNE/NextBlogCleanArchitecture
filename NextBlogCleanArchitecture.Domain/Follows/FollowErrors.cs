using FluentResults;

namespace NextBlogCleanArchitecture.Domain.Follows
{
    public static class FollowErrors
    {
        public static IError CantFollowYourSelf => new Error("You can't follow yourself");
        public static IError AlreadyFollowing => new Error("Already following this user.");
        public static IError NotFollowing => new Error("Not following this user.");
    }
}
