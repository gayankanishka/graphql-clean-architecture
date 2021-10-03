using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendeeById
{
    public record GetAttendeeByIdQuery([ID(nameof(Attendee))] int Id) : IRequest<Attendee>;
}