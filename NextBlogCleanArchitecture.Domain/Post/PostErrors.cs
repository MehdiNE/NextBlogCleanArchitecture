using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;

public static class PostErrors
{
    public static Error OnlyPublished = new(
        "Post.OnlyPublished",
        "Only Published can be archived.");
}