using ConferencePlanner.Application.Tracks.Queries.GetTrackById;
using ConferencePlanner.Domain.Entities;
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
        // [UseUpperCase]
        // public string GetName([Parent] Track track) => track.Name!;
        //
        // [UseApplicationDbContext]
        // [UsePaging(ConnectionName = "TrackSessions")]
        // public IQueryable<Session> GetSessions(
        //     [Parent] Track track,
        //     [ScopedService] ApplicationDbContext dbContext)
        //     => dbContext.Tracks.Where(t => t.Id == track.Id).SelectMany(t => t.Sessions);

        [NodeResolver]
        public static Task<Track> GetTrackByIdAsync(
            GetTrackByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => mediator.Send(input, cancellationToken); 
    }
}