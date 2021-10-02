using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Application.Tracks.Queries.GetTracksByNames
{
    public class GetTracksByNamesQueryHandler :IRequestHandler<GetTracksByNamesQuery, IEnumerable<Track>>
    {
        private readonly ITrackRepository _repository;

        public GetTracksByNamesQueryHandler(ITrackRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Track>> Handle(GetTracksByNamesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllTracks()
                .Where(t => request.Names.Contains(t.Name))
                .ToListAsync(cancellationToken);
        }
    }
}

