using FluentResults;

namespace NextBlogCleanArchitecture.Domain.Posts;

public static class PostErrors
{
    public static IError OnlyPublished => new Error("Only Published can be archived.");
    public static IError NotFound => new Error("Post not found");
}