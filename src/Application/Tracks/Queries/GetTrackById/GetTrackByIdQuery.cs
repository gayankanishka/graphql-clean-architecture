using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTrackById;

public record GetTrackByIdQuery([ID(nameof(Track))] int Id) : IRequest<Track>;