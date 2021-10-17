using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionsByAttendee
{
    public class GetSessionsByAttendeeQueryHandler : IRequestHandler<GetSessionsByAttendeeQuery, IEnumerable<Session>>
    {
        private readonly ISessionByIdDataLoader _dataLoader;
        private readonly IAttendeeRepository _repository;

        public GetSessionsByAttendeeQueryHandler(ISessionByIdDataLoader dataLoader, IAttendeeRepository repository)
        {
            _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<Session>> Handle(GetSessionsByAttendeeQuery request,
            CancellationToken cancellationToken)
        {
            int[] speakerIds = await _repository.GetAllAttendees()
                .Where(a => a.Id == request.Id)
                .Include(a => a.SessionsAttendees)
                .SelectMany(a => a.SessionsAttendees.Select(t => t.SessionId))
                .ToArrayAsync(cancellationToken);

            return await _dataLoader.LoadAsync(speakerIds, cancellationToken);
        }
    }
}