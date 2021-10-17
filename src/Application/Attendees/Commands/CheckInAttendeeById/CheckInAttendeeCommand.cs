using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Commands.CheckInAttendeeById
{
    public record CheckInAttendeeCommand(
        [property: ID(nameof(Session))] int SessionId,
        [property: ID(nameof(Attendee))] int AttendeeId) : IRequest<Attendee?>;
}