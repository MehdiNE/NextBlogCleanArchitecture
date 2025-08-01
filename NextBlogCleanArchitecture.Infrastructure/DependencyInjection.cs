using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NextBlogCleanArchitecture.Application.Abstractions;
using NextBlogCleanArchitecture.Infrastructure.Common.Persistence;
using NextBlogCleanArchitecture.Infrastructure.Posts.Persistence;

namespace NextBlogCleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<NextBlogDbContext>(options =>
                options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NextBlogCA;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False")
            );

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<NextBlogDbContext>());

            return services;
        }
    }
}
