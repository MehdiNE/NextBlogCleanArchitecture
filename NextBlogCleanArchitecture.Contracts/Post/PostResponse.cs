namespace NextBlogCleanArchitecture.Contracts.Post
{
    public class PostResponse
    {
        public Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Content { get; init; }
        public required string AuthorName { get; init; }
        public DateTime CreatedAt { get; init; }
        public PostStatus PostStatus { get; init; }

    }
}
