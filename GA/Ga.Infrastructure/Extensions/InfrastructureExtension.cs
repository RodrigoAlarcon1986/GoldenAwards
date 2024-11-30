using Ga.Infrastructure.Data.Context;
using Ga.Infrastructure.Data.Repositories;
using Ga.Infrastructure.Options;
using GA.Domain.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Ga.Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddGaInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<InfrastructureOptions>(configuration.GetSection("InfrastructureOptions"));

            // Repositories
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddDbContext<GaContext>((sp, opts) =>
            {
                var dbConfig = sp.GetRequiredService<IOptions<InfrastructureOptions>>().Value;

                opts
                    .UseInMemoryDatabase(dbConfig.DbAppName) // Just for project purposes
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Scoped);

            return services;
        }
    }
}
