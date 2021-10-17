using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionById
{
    public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, Session>
    {
        private readonly ISessionByIdDataLoader _dataLoader;

        public GetSessionByIdQueryHandler(ISessionByIdDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public async Task<Session> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}