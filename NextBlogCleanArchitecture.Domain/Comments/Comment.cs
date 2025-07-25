using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextBlogCleanArchitecture.Domain.Comments
{
    public class Comment
    {
        public Guid Id { get; private set; }
        public Guid PostId { get; private set; }
        public Guid AuthorId { get; private set; }
        public string Text { get; private set; }
        public DateTime CreatedAt { get; private set; }


        internal Comment(Guid postId, Guid authorId, string text)
        {
            Id = Guid.NewGuid();
            PostId = postId;
            AuthorId = authorId;
            Text = text;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
