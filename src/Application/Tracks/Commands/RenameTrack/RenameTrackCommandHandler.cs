using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Commands.RenameTrack;

public class RenameTrackCommandHandler : IRequestHandler<RenameTrackCommand, Track?>
{
    private readonly ITrackRepository _repository;

    public RenameTrackCommandHandler(ITrackRepository repository)
    {
        _repository = repository;
    }

    public async Task<Track?> Handle(RenameTrackCommand request, CancellationToken cancellationToken)
    {
        var track = await _repository.FindTrackByIdAsync(request.Id, cancellationToken);

        if (track is null) return track;

        track.Name = request.Name;

        await _repository.UpdateTrackAsync(track, cancellationToken);
        return track;
    }
}