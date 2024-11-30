using GA.Domain.Services.Movies;
using Microsoft.Extensions.DependencyInjection;

namespace GA.Domain.Extensions
{
    public static class DomainExtension
    {
        public static IServiceCollection AddGaDomain(this IServiceCollection services)
        {
            services.AddScoped<IMoviesService, MoviesService>();

            return services;
        }
    }
}
