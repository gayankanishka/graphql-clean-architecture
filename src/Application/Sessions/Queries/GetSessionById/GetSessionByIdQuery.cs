using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionById
{
    public record GetSessionByIdQuery([ID(nameof(Session))] int Id) : IRequest<Session>;
}