using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Common.Interfaces;

public interface ITrackRepository
{
    Task AddTrackAsync(Track track, CancellationToken cancellationToken);
    IQueryable<Track> GetAllTracks();
    Task<Track?> FindTrackByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateTrackAsync(Track track, CancellationToken cancellationToken);
}