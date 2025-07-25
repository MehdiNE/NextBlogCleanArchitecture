using NextBlogCleanArchitecture.Domain.Abstractions;
using NextBlogCleanArchitecture.Domain.Users.Events;

namespace NextBlogCleanArchitecture.Domain.Users
{
    public sealed class User : Entity
    {
        private User(Guid id, string firstName, string lastName) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }


        public static User Create(string firstName, string lastName)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName);

            user.RaiseDomainEvents(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
