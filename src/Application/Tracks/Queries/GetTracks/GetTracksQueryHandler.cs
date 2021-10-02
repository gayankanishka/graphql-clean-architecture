using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTracks
{
    public class GetTracksQueryHandler : IRequestHandler<GetTracksQuery, IQueryable<Track>>
    {
        private readonly ITrackRepository _repository;

        public GetTracksQueryHandler(ITrackRepository repository)
        {
            _repository = repository;
        }

        public async Task<IQueryable<Track>> Handle(GetTracksQuery request, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
                _repository.GetAllTracks().OrderBy(t => t.Name), cancellationToken);
        }
    }
}