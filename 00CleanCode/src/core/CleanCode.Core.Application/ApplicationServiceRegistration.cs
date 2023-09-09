using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanCode.Core.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddAutoMapper(assembly);
        services.AddMediatR(conf =>
            conf.RegisterServicesFromAssemblies(assembly));

        return services;
    }
}
