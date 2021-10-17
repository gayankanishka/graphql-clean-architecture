using ConferencePlanner.Application.Tracks.Commands.AddTrack;
using ConferencePlanner.Application.Tracks.Commands.RenameTrack;
using ConferencePlanner.Domain.Entities;
using ConferencePlanner.Infrastructure.Persistence;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class TrackMutations
    {
        public async Task<AddTrackPayload> AddTrackAsync(
            AddTrackCommand input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            var track = await mediator.Send(input, cancellationToken);

            return new AddTrackPayload(track);
        }

        public async Task<RenameTrackPayload> RenameTrackAsync(
            RenameTrackCommand input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            var track = await mediator.Send(input, cancellationToken);

            if (track is null)
            {
                throw new GraphQLException("Track not found.");
            }

            return new RenameTrackPayload(track);
        }
    }
}