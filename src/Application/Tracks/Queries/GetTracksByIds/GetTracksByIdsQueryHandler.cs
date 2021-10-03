using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTracksByIds
{
    public class GetTracksByIdsQueryHandler : IRequestHandler<GetTracksByIdsQuery, IEnumerable<Track>>
    {
        private readonly ITrackByIdDataLoader _dataLoader;

        public GetTracksByIdsQueryHandler(ITrackByIdDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public async Task<IEnumerable<Track>> Handle(GetTracksByIdsQuery request, CancellationToken cancellationToken)
        {
            return await _dataLoader.LoadAsync(request.Ids, cancellationToken);
        }
    }
}