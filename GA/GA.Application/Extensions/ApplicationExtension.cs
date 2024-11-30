using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GA.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddGaApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
