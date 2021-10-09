using ConferencePlanner.Application.Sessions.Queries.GetSessionsByTrack;
using ConferencePlanner.Application.Tracks.Queries.GetTrackById;
using ConferencePlanner.Domain.Entities;
using ConferencePlanner.GraphQL.Extensions;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.GraphQL.Nodes
{
    [Node]
    [ExtendObjectType(typeof(Track))]
    public class TrackNode
    {
        [UseUpperCase]
        public string GetName([Parent] Track track) => track.Name!;

        [UsePaging(ConnectionName = "TrackSessions")]
        public Task<IQueryable<Session>> GetSessions(
            [Parent] Track track,
            [Service] IMediator mediator)
            => mediator.Send(new GetSessionsByTrackQuery(track.Id));

        [NodeResolver]
        public static Task<Track> GetTrackByIdAsync(
            GetTrackByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => mediator.Send(input, cancellationToken);
    }
}