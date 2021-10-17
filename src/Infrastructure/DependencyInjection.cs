using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Infrastructure.Persistence;
using ConferencePlanner.Infrastructure.Persistence.DataLoaders;
using ConferencePlanner.Infrastructure.Persistence.Repositories;
using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConferencePlanner.Infrastructure;

/// <summary>
/// Dependency injection extension to configure Infrastructure layer services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configure Infrastructure layer services.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPooledDbContextFactory<ApplicationDbContext>(
            (s, o) => o
                .UseSqlite(configuration.GetConnectionString("LocalDbConnection"))
                .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));

        services.AddTransient<IAttendeeRepository>(_ =>
                new AttendeeRepository(
                    _.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext()))
            .AddTransient<ISessionRepository>(_ =>
                new SessionRepository(
                    _.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext()))
            .AddTransient<ISpeakerRepository>(_ =>
                new SpeakerRepository(
                    _.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext()))
            .AddTransient<ITrackRepository>(_ =>
                new TrackRepository(
                    _.GetRequiredService<IDbContextFactory<ApplicationDbContext>>().CreateDbContext()));

        return services;
    }

    public static IRequestExecutorBuilder AddDataLoaders(this IRequestExecutorBuilder builder)
    {
        builder.AddDataLoader<IAttendeeByIdDataLoader, AttendeeByIdDataLoader>()
            .AddDataLoader<ISessionByIdDataLoader, SessionByIdDataLoader>()
            .AddDataLoader<ISpeakerByIdDataLoader, SpeakerByIdDataLoader>()
            .AddDataLoader<ITrackByIdDataLoader, TrackByIdDataLoader>()
            .AddDataLoader<ISessionBySpeakerIdDataLoader, SessionBySpeakerIdDataLoader>()
            .AddDataLoader<ISpeakerBySessionIdDataLoader, SpeakerBySessionIdDataLoader>();

        return builder;
    }
}