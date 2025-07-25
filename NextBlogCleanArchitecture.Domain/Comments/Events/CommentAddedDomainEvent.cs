using NextBlogCleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextBlogCleanArchitecture.Domain.Comments.Events
{
    public sealed record CommentAddedDomainEvent(Guid UserId) : IDomainEvent;
}
