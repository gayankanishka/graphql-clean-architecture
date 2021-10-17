using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionsByIds;

public class GetSessionsByIdsQueryHandler : IRequestHandler<GetSessionsByIdsQuery, IEnumerable<Session>>
{
    private readonly ISessionByIdDataLoader _dataLoader;

    public GetSessionsByIdsQueryHandler(ISessionByIdDataLoader dataLoader)
    {
        _dataLoader = dataLoader;
    }

    public async Task<IEnumerable<Session>> Handle(GetSessionsByIdsQuery request,
        CancellationToken cancellationToken)
    {
        return await _dataLoader.LoadAsync(request.Ids, cancellationToken);
    }
}