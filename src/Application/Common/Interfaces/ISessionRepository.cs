using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Common.Interfaces;

public interface ISessionRepository
{
    Task AddSessionAsync(Session session, CancellationToken cancellationToken);
    IQueryable<Session> GetAllSessions();
    Task<Session?> FindSessionByIdAsync(string id, CancellationToken cancellationToken);
    Task UpdateSessionAsync(Session session, CancellationToken cancellationToken);
}