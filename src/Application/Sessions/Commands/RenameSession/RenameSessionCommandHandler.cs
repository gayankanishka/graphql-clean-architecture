using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.RenameSession
{
    public class RenameSessionCommandHandler : IRequestHandler<RenameSessionCommand, Session?>
    {
        private readonly ISessionRepository _repository;

        public RenameSessionCommandHandler(ISessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Session?> Handle(RenameSessionCommand request, CancellationToken cancellationToken)
        {
            var session = await _repository.FindSessionByIdAsync(request.SessionId, cancellationToken);

            if (session is null)
            {
                return session;
            }

            session.Title = request.Title;

            await _repository.UpdateSessionAsync(session, cancellationToken);
            return session;
        }
    }
}

