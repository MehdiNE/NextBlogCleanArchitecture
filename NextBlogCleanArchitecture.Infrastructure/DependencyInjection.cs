using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Infrastructure.Auth;
using NextBlogCleanArchitecture.Infrastructure.Comments.Persistence;
using NextBlogCleanArchitecture.Infrastructure.Common.Persistence;
using NextBlogCleanArchitecture.Infrastructure.Posts.Persistence;
using NextBlogCleanArchitecture.Infrastructure.Users.Persistence;

namespace NextBlogCleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<NextBlogDbContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NextBlogCa;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            );

            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
                       .AddEntityFrameworkStores<NextBlogDbContext>()
                       .AddDefaultTokenProviders();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<NextBlogDbContext>());

            return services;
        }
    }
}
