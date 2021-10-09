using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendeesBySessionId
{
    public class GetAttendeesBySessionIdQueryHandler : IRequestHandler<GetAttendeesBySessionIdQuery,
        IQueryable<Attendee>>
    {
        private readonly ISessionRepository _repository;

        public GetAttendeesBySessionIdQueryHandler(ISessionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IQueryable<Attendee>> Handle(GetAttendeesBySessionIdQuery request,
            CancellationToken cancellationToken)
        {
            return _repository.GetAllSessions()
                .Where(s => s.Id == request.Id)
                .Include(s => s.SessionAttendees)
                .SelectMany(s => s.SessionAttendees.Select(t => t.Attendee!));
        }
    }
}

