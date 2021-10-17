using ConferencePlanner.Application.Attendees.Queries.GetAttendeeById;
using ConferencePlanner.Application.Attendees.Queries.GetCheckInCount;
using ConferencePlanner.Application.Sessions.Queries.GetSessionById;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Attendees;

public class SessionAttendeeCheckIn
{
    public SessionAttendeeCheckIn(int attendeeId, int sessionId)
    {
        AttendeeId = attendeeId;
        SessionId = sessionId;
    }

    [ID(nameof(Attendee))] public int AttendeeId { get; }

    [ID(nameof(Session))] public int SessionId { get; }

    public async Task<int> CheckInCountAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetCheckInCountQuery(SessionId), cancellationToken);
    }

    public async Task<Attendee> GetAttendeeAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAttendeeByIdQuery(AttendeeId), cancellationToken);
    }

    public async Task<Session> GetSessionAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetSessionByIdQuery(SessionId), cancellationToken);
    }
}