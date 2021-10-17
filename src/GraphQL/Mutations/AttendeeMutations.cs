using ConferencePlanner.Application.Attendees.Commands.CheckInAttendeeById;
using ConferencePlanner.Application.Attendees.Commands.RegisterAttendee;
using ConferencePlanner.Domain.Common;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Mutations;

[ExtendObjectType(OperationTypeNames.Mutation)]
public class AttendeeMutations
{
    public async Task<RegisterAttendeePayload> RegisterAttendeeAsync(
        RegisterAttendeeCommand input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var attendee = await mediator.Send(input, cancellationToken);
        return new RegisterAttendeePayload(attendee);
    }

    public async Task<CheckInAttendeePayload> CheckInAttendeeAsync(
        CheckInAttendeeCommand command,
        [Service] IMediator mediator,
        [Service] ITopicEventSender eventSender,
        CancellationToken cancellationToken)
    {
        var attendee = await mediator.Send(command, cancellationToken);

        if (attendee is null)
            return new CheckInAttendeePayload(
                new UserError("Attendee not found.", "ATTENDEE_NOT_FOUND"));

        await eventSender.SendAsync(
            "OnAttendeeCheckedIn_" + command.SessionId,
            command.AttendeeId,
            cancellationToken);

        return new CheckInAttendeePayload(attendee, command.SessionId);
    }
}