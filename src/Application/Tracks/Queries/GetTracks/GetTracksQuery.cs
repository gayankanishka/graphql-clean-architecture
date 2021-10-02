using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Queries.GetTracks
{
    public record GetTracksQuery() : IRequest<IQueryable<Track>>;
}

