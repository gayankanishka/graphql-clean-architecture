using ConferencePlanner.Application.Attendees.Queries.GetAttendeesBySessionId;
using ConferencePlanner.Application.Sessions.Queries.GetSessionById;
using ConferencePlanner.Application.Speakers.Queries.GetSpeakersBySessionId;
using ConferencePlanner.Application.Tracks.Queries.GetTrackById;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.GraphQL.Nodes
{
    [Node]
    [ExtendObjectType(typeof(Session))]
    public class SessionNode
    {
        [BindMember(nameof(Session.SessionSpeakers), Replace = true)]
        public Task<IEnumerable<Speaker>> GetSpeakersAsync(
            [Parent] Session session,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => mediator.Send(new GetSpeakersBySessionIdQuery(session.Id), cancellationToken);

        [UsePaging(ConnectionName = "SessionAttendees")]
        [BindMember(nameof(Session.SessionAttendees), Replace = true)]
        public Task<IQueryable<Attendee>> GetAttendees(
            [Parent] Session session,
            [Service] IMediator mediator)
            => mediator.Send(new GetAttendeesBySessionIdQuery(session.Id));

        public async Task<Track?> GetTrackAsync(
            [Parent] Session session,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => session.TrackId is not null
                ? await mediator.Send(new GetTrackByIdQuery(session.TrackId.Value), cancellationToken)
                : null;

        [NodeResolver]
        public static Task<Session> GetSessionByIdAsync(
            GetSessionByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => mediator.Send(input, cancellationToken);
    }
}