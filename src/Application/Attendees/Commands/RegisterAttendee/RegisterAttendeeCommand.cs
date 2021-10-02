using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Commands.RegisterAttendee
{
    public record RegisterAttendeeCommand(
        string FirstName,
        string LastName,
        string UserName,
        string EmailAddress) : IRequest<Attendee>;
}