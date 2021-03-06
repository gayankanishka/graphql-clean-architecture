using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.DataLoaders;

internal class TrackByIdDataLoader : BatchDataLoader<int, Track>, ITrackByIdDataLoader
{
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public TrackByIdDataLoader(
        IDbContextFactory<ApplicationDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory ??
                            throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<IReadOnlyDictionary<int, Track>> LoadBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.Tracks
            .Where(s => keys.Contains(s.Id))
            .ToDictionaryAsync(t => t.Id, cancellationToken);
    }
}