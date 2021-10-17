using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionsByIds;

public record GetSessionsByIdsQuery([ID(nameof(Session))] int[] Ids) : IRequest<IEnumerable<Session>>;