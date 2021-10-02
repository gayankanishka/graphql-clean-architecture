using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Commands.RenameTrack
{
    public record RenameTrackCommand(
        [property: ID(nameof(Track))] int Id, 
        string Name) : IRequest<Track?>;
}