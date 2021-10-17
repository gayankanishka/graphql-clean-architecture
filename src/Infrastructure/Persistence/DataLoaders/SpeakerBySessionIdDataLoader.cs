using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.DataLoaders;

internal class SpeakerBySessionIdDataLoader : GroupedDataLoader<int, Speaker>, ISpeakerBySessionIdDataLoader
{
    private static readonly string _speakerCacheKey = GetCacheKeyType<SpeakerByIdDataLoader>();
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    public SpeakerBySessionIdDataLoader(
        IDbContextFactory<ApplicationDbContext> dbContextFactory,
        IBatchScheduler batchScheduler,
        DataLoaderOptions options)
        : base(batchScheduler, options)
    {
        _dbContextFactory = dbContextFactory ??
                            throw new ArgumentNullException(nameof(dbContextFactory));
    }

    protected override async Task<ILookup<int, Speaker>> LoadGroupedBatchAsync(
        IReadOnlyList<int> keys,
        CancellationToken cancellationToken)
    {
        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        var list = await dbContext.Sessions
            .Where(s => keys.Contains(s.Id))
            .Include(s => s.SessionSpeakers)
            .SelectMany(s => s.SessionSpeakers)
            .Include(s => s.Speaker)
            .ToListAsync(cancellationToken);

        TryAddToCache(_speakerCacheKey, list, item => item.SpeakerId, item => item.Speaker!);

        return list.ToLookup(t => t.SessionId, t => t.Speaker!);
    }

    public Task<Speaker> LoadAsync(int key, CancellationToken cancellationToken = new())
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<Speaker>> LoadAsync(IReadOnlyCollection<int> keys,
        CancellationToken cancellationToken = new())
    {
        var result = await base.LoadAsync(keys, cancellationToken);
        var speakers = new List<Speaker>();

        foreach (var r in result) speakers.AddRange(r);

        return speakers;
    }

    public void Set(int key, Task<Speaker> value)
    {
        throw new NotImplementedException();
    }
}