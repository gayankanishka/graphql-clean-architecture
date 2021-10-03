using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Common.Interfaces
{
    public interface ISpeakerRepository
    {
        Task AddSpeakerAsync(Speaker speaker, CancellationToken cancellationToken);
        IQueryable<Speaker> GetAllSpeakers();
        Task<Speaker?> FindSpeakerByIdAsync(int id, CancellationToken cancellationToken);
        Task UpdateSpeakerAsync(Speaker speaker, CancellationToken cancellationToken);
    }
}