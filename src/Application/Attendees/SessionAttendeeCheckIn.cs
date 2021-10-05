using ConferencePlanner.Application.Attendees.Queries.GetAttendeeById;
using ConferencePlanner.Application.Sessions.Queries.GetSessionById;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Attendees
{
    public class SessionAttendeeCheckIn
    {
        public SessionAttendeeCheckIn(int attendeeId, int sessionId)
        {
            AttendeeId = attendeeId;
            SessionId = sessionId;
        }

        [ID(nameof(Attendee))]
        public int AttendeeId { get; }

        [ID(nameof(Session))]
        public int SessionId { get; }

        // [UseApplicationDbContext]
        // public async Task<int> CheckInCountAsync(
        //     [ScopedService] ApplicationDbContext context,
        //     CancellationToken cancellationToken) 
        //     => await context.Sessions
        //         .Where(session => session.Id == SessionId)
        //         .SelectMany(session => session.SessionAttendees)
        //         .CountAsync(cancellationToken);
        //
        public async Task<Attendee> GetAttendeeAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken) 
            => await mediator.Send(new GetAttendeeByIdQuery(AttendeeId), cancellationToken);
        
        public async Task<Session> GetSessionAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken) 
            => await mediator.Send(new GetSessionByIdQuery(SessionId), cancellationToken);
    }
}