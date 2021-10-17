using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTracksByIds
{
    public record GetTracksByIdsQuery([ID(nameof(Track))] int[] Ids) : IRequest<IEnumerable<Track>>;
}