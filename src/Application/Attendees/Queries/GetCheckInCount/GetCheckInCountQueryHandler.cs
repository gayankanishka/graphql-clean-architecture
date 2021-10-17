using ConferencePlanner.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Application.Attendees.Queries.GetCheckInCount
{
    public class GetCheckInCountQueryHandler : IRequestHandler<GetCheckInCountQuery, int>
    {
        private readonly ISessionRepository _repository;

        public GetCheckInCountQueryHandler(ISessionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> Handle(GetCheckInCountQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllSessions()
                .Where(session => session.Id == request.Id)
                .SelectMany(session => session.SessionAttendees)
                .CountAsync(cancellationToken);
        }
    }
}