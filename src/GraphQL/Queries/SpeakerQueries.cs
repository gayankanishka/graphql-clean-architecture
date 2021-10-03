using ConferencePlanner.Application.Speakers.Queries.GetSpeakerById;
using ConferencePlanner.Application.Speakers.Queries.GetSpeakers;
using ConferencePlanner.Application.Speakers.Queries.GetSpeakersByIds;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class SpeakerQueries
    {
        [UsePaging]
        public async Task<IQueryable<Speaker>> GetSpeakers(
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(new GetSpeakersQuery(), cancellationToken);

        public async Task<Speaker> GetSpeakerByIdAsync(
            GetSpeakerByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(input, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            GetSpeakersByIdsQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(input, cancellationToken);
    }
}