using System.Security.Claims;

namespace NextBlogCleanArchitecture.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the UserId claim as a Guid.
        /// Throws UnauthorizedAccessException if the claim is missing or is not a valid Guid.
        /// </summary>
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            // Attempt to find the claim
            var value = user.FindFirst("UserId")?.Value;
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new UnauthorizedAccessException("User ID claim is missing.");
            }

            // Try to parse it as a GUID
            if (Guid.TryParse(value, out var userId))
            {
                return userId;
            }
            else
            {
                throw new UnauthorizedAccessException("User ID claim is not a valid GUID.");
            }
        }
    }
}
