using ConferencePlanner.Application.Attendees.Queries.GetAttendeeById;
using ConferencePlanner.Application.Attendees.Queries.GetAttendees;
using ConferencePlanner.Application.Attendees.Queries.GetAttendeesByIds;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Queries;

[ExtendObjectType(OperationTypeNames.Query)]
public class AttendeeQueries
{
    /// <summary>
    /// Gets all attendees of this conference.
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [UsePaging]
    public async Task<IQueryable<Attendee>> GetAttendeesAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAttendeesQuery(), cancellationToken);
    }

    /// <summary>
    /// Gets an attendee by its identifier.
    /// </summary>
    /// <param name="input">The attendee identifier.</param>
    /// <param name="mediator"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Attendee> GetAttendeeByIdAsync(
        GetAttendeeByIdQuery input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }

    public async Task<IEnumerable<Attendee>> GetAttendeesByIdAsync(
        GetAttendeesByIdsQuery input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(input, cancellationToken);
    }
}