using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Domain.Users;
using System.Security.Claims;
using System.Text;

namespace NextBlogCleanArchitecture.Infrastructure.Auth
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly JwtAuthOptions _jwtAuthOptions;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(UserManager<AppUser> userManager, IOptions<JwtAuthOptions> jwtAuthOptions, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _jwtAuthOptions = jwtAuthOptions.Value;
            _signInManager = signInManager;
        }

        public async Task<string> GenerateTokenAsync(User user)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (appUser is null)
            {
                return "";
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", appUser.Id.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuthOptions.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtAuthOptions.ExpirationInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtAuthOptions.Issuer,
                Audience = _jwtAuthOptions.Audience,
            };

            var handler = new JsonWebTokenHandler();

            var accessToken = handler.CreateToken(tokenDescriptor);

            return accessToken;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            if (appUser == null) return false;

            var result = await _signInManager.PasswordSignInAsync(appUser, password, false, false);
            if (!result.Succeeded) return false;

            return true;
        }

        public async Task<IdentityResult> RegisterAsync(AppUser identityUser, string password)
        {
            return await _userManager.CreateAsync(identityUser, password);
        }
    }
}
