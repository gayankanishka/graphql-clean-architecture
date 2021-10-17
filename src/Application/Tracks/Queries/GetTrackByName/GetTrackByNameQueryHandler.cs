using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTrackByName;

public class GetTrackByNameQueryHandler : IRequestHandler<GetTrackByNameQuery, Track?>
{
    private readonly ITrackRepository _repository;

    public GetTrackByNameQueryHandler(ITrackRepository repository)
    {
        _repository = repository;
    }

    public async Task<Track?> Handle(GetTrackByNameQuery request, CancellationToken cancellationToken)
    {
        return await Task.Run(() =>
        {
            return _repository
                .GetAllTracks()
                .FirstOrDefault(_ => _.Name == request.Name);
        }, cancellationToken);
    }
}