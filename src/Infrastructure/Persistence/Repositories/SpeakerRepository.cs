using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Infrastructure.Persistence.Repositories;

internal class SpeakerRepository : ISpeakerRepository
{
    private readonly ApplicationDbContext _context;

    public SpeakerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddSpeakerAsync(Speaker speaker, CancellationToken cancellationToken)
    {
        await _context.Speakers.AddAsync(speaker, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public IQueryable<Speaker> GetAllSpeakers()
    {
        return _context.Speakers
            .AsQueryable()
            .AsNoTracking();
    }

    public async Task<Speaker?> FindSpeakerByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Speakers.FirstOrDefaultAsync(
            t => t.Id == id, cancellationToken);
    }

    public async Task UpdateSpeakerAsync(Speaker speaker, CancellationToken cancellationToken)
    {
        _context.Speakers.Update(speaker);
        await _context.SaveChangesAsync(cancellationToken);
    }
}