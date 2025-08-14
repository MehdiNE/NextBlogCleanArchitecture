

using FluentResults;

namespace NextBlogCleanArchitecture.Domain.Comments
{
    public static class CommentErrors
    {
        public static IError NotFound => new Error("Comment Not Found.");
    }
}
