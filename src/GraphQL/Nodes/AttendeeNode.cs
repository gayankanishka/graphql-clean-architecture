using ConferencePlanner.Application.Attendees.Queries.GetAttendeeById;
using ConferencePlanner.Application.Sessions.Queries.GetSessionsByAttendeeQuery;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.GraphQL.Nodes
{
    [Node]
    [ExtendObjectType]
    public class AttendeeNode
    {
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [Parent] Attendee attendee,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(new GetSessionsByAttendeeQuery(attendee.Id), cancellationToken);

        [NodeResolver]
        public static Task<Attendee> GetAttendeeAsync(
            GetAttendeeByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => mediator.Send(input, cancellationToken);
    }
}