namespace NextBlogCleanArchitecture.Contracts.Post
{
    public class CreatePostRequest
    {
        public required string Title { get; init; }
        public required string Content { get; init; }
    }
}
