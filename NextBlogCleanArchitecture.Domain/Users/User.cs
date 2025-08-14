using NextBlogCleanArchitecture.Domain.Abstractions;
using NextBlogCleanArchitecture.Domain.Users.Events;

namespace NextBlogCleanArchitecture.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id) : base(id) { }

        public string Username { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public DateTime CreatedAt { get; private init; }

        public static User Create(Guid id, string username, string email)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException("Username is required.");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new ArgumentException("Invalid email.");

            var user = new User(id)
            {
                Username = username,
                Email = email,
                CreatedAt = DateTime.UtcNow
            };

            user.RaiseDomainEvents(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
