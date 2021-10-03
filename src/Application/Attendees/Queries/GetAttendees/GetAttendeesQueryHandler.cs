using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendees
{
    public class GetAttendeesQueryHandler : IRequestHandler<GetAttendeesQuery, IQueryable<Attendee>>
    {
        private readonly IAttendeeRepository _repository;

        public GetAttendeesQueryHandler(IAttendeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<IQueryable<Attendee>> Handle(GetAttendeesQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllAttendees();
        }
    }
}