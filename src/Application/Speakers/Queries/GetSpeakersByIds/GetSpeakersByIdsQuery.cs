using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakersByIds
{
    public record GetSpeakersByIdsQuery([ID(nameof(Speaker))] int[] Ids) : IRequest<IEnumerable<Speaker>>;
}

