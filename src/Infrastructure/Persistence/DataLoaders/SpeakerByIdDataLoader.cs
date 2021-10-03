using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using GreenDonut;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.DataLoaders
{
    internal class SpeakerByIdDataLoader : BatchDataLoader<int, Speaker>, ISpeakerByIdDataLoader
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public SpeakerByIdDataLoader(
            IDbContextFactory<ApplicationDbContext> dbContextFactory,
            IBatchScheduler batchScheduler,
            DataLoaderOptions options)
            : base(batchScheduler, options)
        {
            _dbContextFactory = dbContextFactory ?? 
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Speaker>> LoadBatchAsync(
            IReadOnlyList<int> keys, 
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext = 
                await _dbContextFactory.CreateDbContextAsync(cancellationToken);

            return await dbContext.Speakers
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}