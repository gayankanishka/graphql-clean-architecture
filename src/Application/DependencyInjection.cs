using System.Reflection;
using ConferencePlanner.Application.Common.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ConferencePlanner.Application;

/// <summary>
/// Dependency injection extension to configure Application layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configure Application layer services.
    /// </summary>
    /// <param name="services"></param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
}