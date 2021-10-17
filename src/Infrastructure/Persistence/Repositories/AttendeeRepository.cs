using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.Repositories;

internal class AttendeeRepository : IAttendeeRepository
{
    private readonly ApplicationDbContext _context;

    public AttendeeRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AddAttendeeAsync(Attendee attendee, CancellationToken cancellationToken)
    {
        await _context.Attendees.AddAsync(attendee, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Attendee> GetAllAttendees()
    {
        return _context.Attendees
            .AsQueryable()
            .AsNoTracking();
    }

    public async Task<Attendee?> FindAttendeeByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Attendees.FirstOrDefaultAsync(
            t => t.Id == id, cancellationToken);
    }

    public async Task UpdateAttendeeAsync(Attendee attendee, CancellationToken cancellationToken)
    {
        _context.Attendees.Update(attendee);
        await _context.SaveChangesAsync(cancellationToken);
    }
}