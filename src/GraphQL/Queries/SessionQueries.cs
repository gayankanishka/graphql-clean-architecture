using ConferencePlanner.Application.Sessions;
using ConferencePlanner.Application.Sessions.Queries.GetSessionById;
using ConferencePlanner.Application.Sessions.Queries.GetSessions;
using ConferencePlanner.Application.Sessions.Queries.GetSessionsByIds;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Queries
{
    [ExtendObjectType(OperationTypeNames.Query)]
    public class SessionQueries
    {
        [UsePaging]
        [UseFiltering(typeof(SessionFilterInputType))]
        [UseSorting]
        public async Task<IQueryable<Session>> GetSessionsAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(new GetSessionsQuery(), cancellationToken);

        public async Task<Session> GetSessionByIdAsync(
            GetSessionByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(input, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            GetSessionsByIdsQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(input, cancellationToken);
    }
}