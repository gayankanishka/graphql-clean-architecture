using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.Repositories
{
    internal class TrackRepository : ITrackRepository
    {
        private readonly ApplicationDbContext _context;

        public TrackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTrackAsync(Track track, CancellationToken cancellationToken)
        {
            await _context.Tracks.AddAsync(track, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        
        public IQueryable<Track> GetAllTracks()
        {
            return _context.Tracks
                .AsQueryable()
                .AsNoTracking();
        }

        public async Task<Track?> FindTrackByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Tracks.FirstOrDefaultAsync(
                t => t.Id == id, cancellationToken);
        }

        public async Task UpdateTrackAsync(Track track, CancellationToken cancellationToken)
        {
            _context.Tracks.Update(track);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

