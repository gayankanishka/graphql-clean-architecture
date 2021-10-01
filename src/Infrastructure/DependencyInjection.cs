using ConferencePlanner.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConferencePlanner.Infrastructure
{
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
                    .UseSqlite("Data Source=conferences.db")
                    .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()));

            return services;
        }
    }
}