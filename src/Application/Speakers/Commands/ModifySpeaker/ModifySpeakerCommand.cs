using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Commands.ModifySpeaker
{
    public record ModifySpeakerCommand(
        [property: ID(nameof(Speaker))] 
        int Id,
        Optional<string?> Name,
        Optional<string?> Bio,
        Optional<string?> WebSite) : IRequest<Speaker?>;
}