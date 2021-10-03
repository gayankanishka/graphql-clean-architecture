using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.DataLoaders
{
    internal class SessionByIdDataLoader : BatchDataLoader<int, Session>, ISessionByIdDataLoader
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public SessionByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ??
                                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Session>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext =
                await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            return await dbContext.Sessions
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}