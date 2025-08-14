using FluentResults;

namespace NextBlogCleanArchitecture.Domain.Users
{
    public class UserErrors
    {
        public static IError NotFound => new Error("User not found");
        public static IError UserExist => new Error("User already exist");
        public static IError InvalidCredentials => new Error("Invalid Credentials");
    }
}
