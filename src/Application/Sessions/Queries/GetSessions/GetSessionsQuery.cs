using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessions
{
    public record GetSessionsQuery() : IRequest<IQueryable<Session>>;
}

