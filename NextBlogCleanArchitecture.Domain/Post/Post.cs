using Bookify.Domain.Abstractions;
using Bookify.Domain.Bookings;
using NextBlogCleanArchitecture.Domain.Abstractions;
using NextBlogCleanArchitecture.Domain.Comments;
using NextBlogCleanArchitecture.Domain.Comments.Events;

namespace NextBlogCleanArchitecture.Domain.Post
{
    public sealed class Post : Entity
    {
        private Post(Guid id, Guid authorId, string title, string content, PostStatus postStatus, DateTime? publishedAt) : base(id)
        {
            AuthorId = authorId;
            Title = title;
            Content = content;
            PostStatus = postStatus;
            PublishedAt = publishedAt;
        }


        private readonly List<Comment> _comments = new();
        public Guid AuthorId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Content { get; private set; } = string.Empty;
        public PostStatus PostStatus { get; private set; }
        public DateTime? PublishedAt { get; private set; }

        public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();


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

            var post = new Post(Guid.NewGuid(), authorId, title, content, PostStatus.Published, DateTime.UtcNow);

            return post;
        }

        public static Post Draft(Guid authorId, string title, string content)
        {
            var post = new Post(Guid.NewGuid(), authorId, title, content, PostStatus.Drafted, null);

            return post;
        }

        public Result Archive()
        {
            if (PostStatus != PostStatus.Published)
            {
                return Result.Failure(PostErrors.OnlyPublished);
            }

            PostStatus = PostStatus.Archived;

            return Result.Success();
        }

        public Result AddComment(Guid commentAuthorId, string text)
        {
            if (PostStatus != PostStatus.Published)
            {
                return Result.Failure(PostErrors.OnlyPublished);
            }

            var newComment = new Comment(this.Id, commentAuthorId, text);
            _comments.Add(newComment);

            RaiseDomainEvents(new CommentAddedDomainEvent(commentAuthorId));

            return Result.Success();
        }

        public Result DeleteComment(Guid commentAuthorId, Guid commentId)
        {
            var comment = _comments.SingleOrDefault(x => x.Id == commentAuthorId);

            if (comment is null)
            {
                return Result.Failure(CommentErrors.NotFound);
            }

            if (comment.AuthorId != commentAuthorId)
            {
                return Result.Failure(CommentErrors.OnlyAuthor);
            }

            _comments.Remove(comment);

            return Result.Success();
        }
    }
}
