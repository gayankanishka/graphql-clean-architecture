using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Infrastructure.Persistence.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _context;

        public SessionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddSessionAsync(Session session, CancellationToken cancellationToken)
        {
            await _context.Sessions.AddAsync(session, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Session?> FindSessionByIdAsync(string id, CancellationToken cancellationToken)
        {
            return await _context.Sessions.FindAsync(id);
        }

        public async Task UpdateSessionAsync(Session session, CancellationToken cancellationToken)
        {
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}