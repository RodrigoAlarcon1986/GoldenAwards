using Ga.Infrastructure.Extensions;
using Ga.Infrastructure.Options;
using GA.Application.Commands.Movie.SaveMovies;
using GA.Application.Extensions;
using GA.Domain.Extensions;
using MediatR;
using Microsoft.Extensions.Options;

namespace GA.Api.Modules
{
    public static class GaModule
    {
        public static IServiceCollection AddGaModule(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGaApplication();
            services.AddGaDomain();
            services.AddGaInfrastructure(configuration);

            return services;
        }

        public static async Task InitGaModuleAsync(this IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();

            var appConfig = scope.ServiceProvider.GetRequiredService<IOptions<InfrastructureOptions>>().Value;
            var sender = scope.ServiceProvider.GetRequiredService<ISender>();

            await sender.Send(new SaveMoviesCommand { PathToCsvFile = appConfig.PathToCsvFile });
        }
    }
}
