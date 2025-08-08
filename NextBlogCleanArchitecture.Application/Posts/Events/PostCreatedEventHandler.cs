using MediatR;
using NextBlogCleanArchitecture.Domain.Post.Events;

namespace NextBlogCleanArchitecture.Application.Posts.Events
{
    public class PostCreatedEventHandler : INotificationHandler<PostCreatedEvent>
    {
        public Task Handle(PostCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("Post created.");
            return Task.CompletedTask;
        }
    }
}
