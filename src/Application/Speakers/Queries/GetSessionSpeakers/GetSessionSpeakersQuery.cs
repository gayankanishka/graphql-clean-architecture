using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSessionSpeakers;

public record GetSessionSpeakersQuery
    ([property: ID(nameof(Speaker))] int Id) : IRequest<IEnumerable<SessionSpeaker>>;