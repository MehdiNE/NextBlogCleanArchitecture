namespace NextBlogCleanArchitecture.Application.Posts.Commands.CreatePost
{
    public class CreatePostRequest
    {
        public required string Title { get; init; }
        public required string Content { get; init; }
    }
}
