using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakersBySessionId
{
    public record GetSpeakersBySessionIdQuery(
        [property: ID(nameof(Session))] int Id) : IRequest<IEnumerable<Speaker>>;
}