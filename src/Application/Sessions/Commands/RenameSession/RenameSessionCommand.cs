using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.RenameSession
{
    public record RenameSessionCommand(
        [property: ID(nameof(Session))]
        string SessionId,
        string Title) : IRequest<Session?>;
}