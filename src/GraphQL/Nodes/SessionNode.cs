using ConferencePlanner.Application.Sessions.Queries.GetSessionById;
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
        // [BindMember(nameof(Session.SessionSpeakers), Replace = true)]
        // public Task<Speaker[]> GetSpeakersAsync(
        //     [Parent] Session session,
        //     SpeakerBySessionIdDataLoader speakerBySessionId,
        //     CancellationToken cancellationToken)
        //     => speakerBySessionId.LoadAsync(session.Id, cancellationToken);

        // [UsePaging(ConnectionName = "SessionAttendees")]
        // [BindMember(nameof(Session.SessionAttendees), Replace = true)]
        // public IQueryable<Attendee> GetAttendees(
        //     [Parent] Session session,
        //     [ScopedService] ApplicationDbContext dbContext)
        //     => dbContext.Sessions
        //         .Where(s => s.Id == session.Id)
        //         .Include(s => s.SessionAttendees)
        //         .SelectMany(s => s.SessionAttendees.Select(t => t.Attendee!));

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