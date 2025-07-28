using Microsoft.Extensions.DependencyInjection;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Infrastructure.Posts.Persistence;

namespace NextBlogCleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();

            return services;
        }
    }
}
