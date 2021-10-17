using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.AddSession;

public record AddSessionCommand(
    string Title,
    string? Abstract,
    [property: ID(nameof(Speaker))] IReadOnlyList<int> SpeakerIds) : IRequest<Session>;