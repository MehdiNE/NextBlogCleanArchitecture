using FluentResults;
using NextBlogCleanArchitecture.Domain.Abstractions;
using NextBlogCleanArchitecture.Domain.Comments;

namespace NextBlogCleanArchitecture.Domain.Posts
{
    public sealed class Post : Entity
    {
        public Guid AuthorId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public PostStatus PostStatus { get; private set; }
        public DateTime? PublishedAt { get; private set; }
        public ICollection<Comment> Comments { get; private set; } = [];

        private Post(Guid authorId, string title, string content, PostStatus postStatus, DateTime? publishedAt, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            AuthorId = authorId;
            Title = title;
            Content = content;
            PostStatus = postStatus;
            PublishedAt = publishedAt;
        }


        public static Post Publish(Guid authorId, string title, string content)
        {
            // Add Guard Clauses for validation
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty.", nameof(title));
            }
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException("Content cannot be empty.", nameof(content));
            }

            var post = new Post(authorId, title, content, PostStatus.Published, DateTime.UtcNow);

            return post;
        }

        public Result Archive()
        {
            if (PostStatus != PostStatus.Published)
            {
                return Result.Fail(PostErrors.OnlyPublished);
            }

            PostStatus = PostStatus.Archived;
            return Result.Ok();
        }

        private Post() { }

    }
}
