using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionsBySpeakerId;

public record GetSessionsBySpeakerIdQuery(
    [property: ID(nameof(Speaker))] int Id) : IRequest<IEnumerable<Session>>;