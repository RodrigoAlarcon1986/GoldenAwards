using Ga.Infrastructure.Data.Context;
using Ga.Infrastructure.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Ga.Api.IntegrationTest
{
    internal class GaWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly string pathToCsv;

        public GaWebApplicationFactory(string pathToCsv)
        {
            this.pathToCsv = pathToCsv;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(async services =>
            {
                var dbContext = CreateContext(services);
                await dbContext.Database.EnsureDeletedAsync();

                services.Configure<InfrastructureOptions>(opts =>
                {
                    opts.PathToCsvFile = pathToCsv;
                });
            });
        }

        private static GaContext CreateContext(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GaContext>();

            return dbContext;
        }
    }
}
