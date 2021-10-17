using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTracksByNames;

public record GetTracksByNamesQuery(string[] Names) : IRequest<IEnumerable<Track>>;