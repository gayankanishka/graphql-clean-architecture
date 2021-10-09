using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.DataLoaders
{
    internal class SessionBySpeakerIdDataLoader : GroupedDataLoader<int, Session>, ISessionBySpeakerIdDataLoader
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

        public Task<Session> LoadAsync(int key, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Session>> LoadAsync(IReadOnlyCollection<int> keys, 
            CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.LoadAsync(keys, cancellationToken);
            var sessions = new List<Session>();

            foreach (var r in result)
            {
                sessions.AddRange(r);
            }

            return sessions;
        }

        public void Set(int key, Task<Session> value)
        {
            throw new NotImplementedException();
        }
    }
}