using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakerById
{
    public class GetSpeakerByIdQueryHandler : IRequestHandler<GetSpeakerByIdQuery, Speaker>
    {
        private readonly ISpeakerByIdDataLoader _dataLoader;

        public GetSpeakerByIdQueryHandler(ISpeakerByIdDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public async Task<Speaker> Handle(GetSpeakerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataLoader.LoadAsync(request.Id, cancellationToken);
        }
    }
}

