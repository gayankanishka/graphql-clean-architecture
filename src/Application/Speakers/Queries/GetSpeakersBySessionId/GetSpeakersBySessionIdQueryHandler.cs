using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakersBySessionId;

public class GetSpeakersBySessionIdQueryHandler : IRequestHandler<GetSpeakersBySessionIdQuery, IEnumerable<Speaker>>
{
    private readonly ISpeakerBySessionIdDataLoader _dataLoader;

    public GetSpeakersBySessionIdQueryHandler(ISpeakerBySessionIdDataLoader dataLoader)
    {
        _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
    }

    public async Task<IEnumerable<Speaker>> Handle(GetSpeakersBySessionIdQuery request,
        CancellationToken cancellationToken)
    {
        return await _dataLoader.LoadAsync(new[] { request.Id }, cancellationToken);
    }
}