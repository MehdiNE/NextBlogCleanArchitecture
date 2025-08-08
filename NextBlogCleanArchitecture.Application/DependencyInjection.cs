using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NextBlogCleanArchitecture.Application.Common.Behaviors;

namespace NextBlogCleanArchitecture.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection));

                options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));

            return services;
        }
    }
}
