using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Common.Interfaces;

public interface IAttendeeRepository
{
    Task AddAttendeeAsync(Attendee attendee, CancellationToken cancellationToken);
    IQueryable<Attendee> GetAllAttendees();
    Task<Attendee?> FindAttendeeByIdAsync(int id, CancellationToken cancellationToken);
    Task UpdateAttendeeAsync(Attendee attendee, CancellationToken cancellationToken);
}