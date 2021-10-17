using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Commands.RegisterAttendee
{
    public class RegisterAttendeeCommandHandler : IRequestHandler<RegisterAttendeeCommand, Attendee>
    {
        private readonly IAttendeeRepository _repository;

        public RegisterAttendeeCommandHandler(IAttendeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Attendee> Handle(RegisterAttendeeCommand request, CancellationToken cancellationToken)
        {
            var attendee = new Attendee
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailAddress = request.EmailAddress
            };

            await _repository.AddAttendeeAsync(attendee, cancellationToken);

            return attendee;
        }
    }
}