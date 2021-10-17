using ConferencePlanner.Application.Sessions.Queries.GetSessionById;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Subscriptions;

[ExtendObjectType(OperationTypeNames.Subscription)]
public class SessionSubscriptions
{
    [Subscribe]
    [Topic]
    public Task<Session> OnSessionScheduledAsync(
        [EventMessage] int sessionId,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return mediator.Send(new GetSessionByIdQuery(sessionId), cancellationToken);
    }
}