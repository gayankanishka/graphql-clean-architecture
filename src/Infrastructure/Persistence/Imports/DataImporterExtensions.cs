using HotChocolate.Execution.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConferencePlanner.Infrastructure.Persistence.Imports;

public static class ImportRequestExecutorBuilderExtensions
{
    public static IRequestExecutorBuilder EnsureDatabaseIsCreated(
        this IRequestExecutorBuilder builder)
    {
        return builder.ConfigureSchemaAsync(async (services, _, ct) =>
        {
            var factory =
                services.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
            await using var dbContext = await factory.CreateDbContextAsync(ct);

            if (await dbContext.Database.EnsureCreatedAsync(ct))
            {
                var importer = new DataImporter();
                await importer.LoadDataAsync(dbContext);
            }
        });
    }
}