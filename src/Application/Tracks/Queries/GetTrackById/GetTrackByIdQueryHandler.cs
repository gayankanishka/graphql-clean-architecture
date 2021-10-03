using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTrackById
{
    public class GetTrackByIdQueryHandler : IRequestHandler<GetTrackByIdQuery, Track>
    {
        private readonly ITrackByIdDataLoader _dataLoader;

        public GetTrackByIdQueryHandler(ITrackByIdDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public async Task<Track> Handle(GetTrackByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }    
}

