using Microsoft.AspNetCore.Identity;
using NextBlogCleanArchitecture.Domain.Users;

namespace NextBlogCleanArchitecture.Application.Abstractions
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(AppUser identityUser, string password);
        Task<string> GenerateTokenAsync(User user);
        Task<bool> LoginAsync(string email, string password);
    }
}
