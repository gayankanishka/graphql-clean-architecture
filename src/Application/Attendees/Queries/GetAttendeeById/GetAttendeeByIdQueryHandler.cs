using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendeeById
{
    public class GetAttendeeByIdQueryHandler : IRequestHandler<GetAttendeeByIdQuery, Attendee>
    {
        private readonly IAttendeeByIdDataLoader _dataLoader;

        public GetAttendeeByIdQueryHandler(IAttendeeByIdDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public async Task<Attendee> Handle(GetAttendeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}