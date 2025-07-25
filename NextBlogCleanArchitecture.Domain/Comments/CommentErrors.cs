using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextBlogCleanArchitecture.Domain.Comments
{
    public static class CommentErrors
    {
        public static Error NotFound = new(
        "Comment.NotFound",
        "Comment not fount");

        public static Error OnlyAuthor = new(
         "Comment.OnlyAutor",
         "Only comment author can delete comment");

    }
}
