using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionsByTrack;

public record GetSessionsByTrackQuery(
    [property: ID(nameof(Track))] int Id) : IRequest<IQueryable<Session>>;