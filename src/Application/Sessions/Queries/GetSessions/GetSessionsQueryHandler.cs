using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessions
{
    public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, IQueryable<Session>>
    {
        private readonly ISessionRepository _repository;

        public GetSessionsQueryHandler(ISessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IQueryable<Session>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllSessions();
        }
    }
}