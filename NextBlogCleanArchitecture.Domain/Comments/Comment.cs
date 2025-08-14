using NextBlogCleanArchitecture.Domain.Abstractions;
using NextBlogCleanArchitecture.Domain.Posts;

namespace NextBlogCleanArchitecture.Domain.Comments
{
    public class Comment : Entity
    {
        public Guid PostId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Post? Post { get; private set; }


        public Comment(Guid postId, string content, Guid? id = null) : base(id ?? Guid.NewGuid())
        {
            PostId = postId;
            Content = content;
            CreatedAt = DateTime.UtcNow;
        }
        private Comment() { }

    }
}
