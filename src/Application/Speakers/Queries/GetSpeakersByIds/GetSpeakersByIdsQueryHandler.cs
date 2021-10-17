using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakersByIds
{
    public class GetSpeakersByIdsQueryHandler : IRequestHandler<GetSpeakersByIdsQuery, IEnumerable<Speaker>>
    {
        private readonly ISpeakerByIdDataLoader _dataLoader;

        public GetSpeakersByIdsQueryHandler(ISpeakerByIdDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public async Task<IEnumerable<Speaker>> Handle(GetSpeakersByIdsQuery request,
            CancellationToken cancellationToken)
        {
            return await _dataLoader.LoadAsync(request.Ids, cancellationToken);
        }
    }
}