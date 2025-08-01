
using ErrorOr;

namespace Bookify.Domain.Bookings;

public static class PostErrors
{
    public static Error OnlyPublished = Error.Validation(
        "Post.OnlyPublished",
        "Only Published can be archived.");

    public static Error NotFound = Error.NotFound(
        "Post.NotFound",
        "Post not found");
}