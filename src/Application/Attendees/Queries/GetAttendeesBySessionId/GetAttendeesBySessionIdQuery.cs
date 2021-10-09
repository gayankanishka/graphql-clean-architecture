using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendeesBySessionId
{
    public record GetAttendeesBySessionIdQuery(
        [property: ID(nameof(Session))] int Id) : IRequest<IQueryable<Attendee>>;
}