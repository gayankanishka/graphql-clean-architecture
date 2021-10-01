using ConferencePlanner.Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.DataLoaders
{
    public class SessionBySpeakerIdDataLoader : GroupedDataLoader<int, Session>
    {
        private static readonly string _sessionCacheKey = GetCacheKeyType<SessionByIdDataLoader>();
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public SessionBySpeakerIdDataLoader(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<ILookup<int, Session>> LoadGroupedBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext =
                await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            List<SessionSpeaker> list = await dbContext.Speakers
                .Where(s => keys.Contains(s.Id))
                .Include(s => s.SessionSpeakers)
                .SelectMany(s => s.SessionSpeakers)
                .Include(s => s.Session)
                .ToListAsync(cancellationToken);

            TryAddToCache(_sessionCacheKey, list, item => item.SessionId, item => item.Session!);

            return list.ToLookup(t => t.SpeakerId, t => t.Session!);
        }
    }
}