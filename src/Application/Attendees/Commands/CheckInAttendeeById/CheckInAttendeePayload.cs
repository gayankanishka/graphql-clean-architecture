using ConferencePlanner.Application.Sessions.Queries.GetSessionById;
using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;
using ConferencePlanner.Domain.Payloads;
using HotChocolate;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Commands.CheckInAttendeeById
{
    public class CheckInAttendeePayload : AttendeePayloadBase
    {
        private readonly int? _sessionId;

        public CheckInAttendeePayload(Attendee attendee, int sessionId)
            : base(attendee)
        {
            _sessionId = sessionId;
        }

        public CheckInAttendeePayload(UserError error)
            : base(new[] { error })
        {
        }

        public async Task<Session?> GetSessionAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            if (_sessionId.HasValue)
            {
                return await mediator.Send(new GetSessionByIdQuery(_sessionId.Value), cancellationToken);
            }
        
            return null;
        }
    }
}