using ConferencePlanner.Application.Sessions.Commands.AddSession;
using ConferencePlanner.Application.Sessions.Commands.RenameSession;
using ConferencePlanner.Application.Sessions.Commands.ScheduleSession;
using ConferencePlanner.Domain.Common;
using ConferencePlanner.GraphQL.Subscriptions;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class SessionMutations
    {
        public async Task<AddSessionPayload> AddSessionAsync(
            AddSessionCommand input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(input.Title))
            {
                return new AddSessionPayload(
                    new UserError("The title cannot be empty.", "TITLE_EMPTY"));
            }

            if (input.SpeakerIds.Count == 0)
            {
                return new AddSessionPayload(
                    new UserError("No speaker assigned.", "NO_SPEAKER"));
            }

            var session = await mediator.Send(input, cancellationToken);

            return new AddSessionPayload(session);
        }

        public async Task<ScheduleSessionPayload> ScheduleSessionAsync(
            ScheduleSessionCommand input,
            [Service] IMediator mediator,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            if (input.EndTime < input.StartTime)
            {
                return new ScheduleSessionPayload(
                    new UserError("endTime has to be larger than startTime.", "END_TIME_INVALID"));
            }

            var session = await mediator.Send(input, cancellationToken);

            if (session is null)
            {
                return new ScheduleSessionPayload(
                    new UserError("Session not found.", "SESSION_NOT_FOUND"));
            }

            await eventSender.SendAsync(
                nameof(SessionSubscriptions.OnSessionScheduledAsync),
                session.Id);

            return new ScheduleSessionPayload(session);
        }

        public async Task<RenameSessionPayload> RenameSessionAsync(
            RenameSessionCommand input,
            [Service] IMediator mediator,
            [Service]ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var session = await mediator.Send(input, cancellationToken);

            if (session is null)
            {
                return new RenameSessionPayload(
                    new UserError("Session not found.", "SESSION_NOT_FOUND"));
            }
            
            await eventSender.SendAsync(
                nameof(SessionSubscriptions.OnSessionScheduledAsync),
                session.Id);

            return new RenameSessionPayload(session);
        }
    }
}