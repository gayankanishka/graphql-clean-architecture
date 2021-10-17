using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Commands.CheckInAttendeeById
{
    public class CheckInAttendeeCommandHandler : IRequestHandler<CheckInAttendeeCommand, Attendee?>
    {
        private readonly IAttendeeRepository _repository;

        public CheckInAttendeeCommandHandler(IAttendeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Attendee?> Handle(CheckInAttendeeCommand request, CancellationToken cancellationToken)
        {
            var attendee = await _repository.FindAttendeeByIdAsync(request.AttendeeId, cancellationToken);

            if (attendee is null)
            {
                return attendee;
            }

            attendee.SessionsAttendees.Add(
                new SessionAttendee
                {
                    SessionId = request.SessionId
                });

            await _repository.UpdateAttendeeAsync(attendee, cancellationToken);

            return attendee;
        }
    }
}