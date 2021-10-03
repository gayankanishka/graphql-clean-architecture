using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakers
{
    public record GetSpeakersQuery() : IRequest<IQueryable<Speaker>>;
}

