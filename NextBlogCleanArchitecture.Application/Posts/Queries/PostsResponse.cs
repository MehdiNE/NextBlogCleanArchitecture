namespace NextBlogCleanArchitecture.Application.Posts.Queries
{
    public class PostsResponse
    {
        public IEnumerable<PostResponse> Items { get; init; } = [];
    }
}
