using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionsByTrack
{
    public class GetSessionsByTrackQueryHandler : IRequestHandler<GetSessionsByTrackQuery, IQueryable<Session>>
    {
        private readonly ITrackRepository _repository;

        public GetSessionsByTrackQueryHandler(ITrackRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IQueryable<Session>> Handle(GetSessionsByTrackQuery request,
            CancellationToken cancellationToken)
        {
            return _repository.GetAllTracks()
                .Where(t => t.Id == request.Id)
                .SelectMany(t => t.Sessions);
        }
    }
}

