using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.ScheduleSession
{
    public class ScheduleSessionCommandHandler : IRequestHandler<ScheduleSessionCommand, Session?>
    {
        private readonly ISessionRepository _repository;

        public ScheduleSessionCommandHandler(ISessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Session?> Handle(ScheduleSessionCommand request, CancellationToken cancellationToken)
        {
            var session = new Session()
            {
                TrackId = request.TrackId,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            await _repository.UpdateSessionAsync(session, cancellationToken);

            return session;
        }
    }
}

