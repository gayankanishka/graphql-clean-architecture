using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTrackByName;

public record GetTrackByNameQuery(string Name) : IRequest<Track?>;