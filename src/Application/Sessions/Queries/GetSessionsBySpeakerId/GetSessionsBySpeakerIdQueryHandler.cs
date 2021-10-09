using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Queries.GetSessionsBySpeakerId
{
    public class GetSessionsBySpeakerIdQueryHandler : IRequestHandler<GetSessionsBySpeakerIdQuery, IEnumerable<Session>>
    {
        private readonly ISessionBySpeakerIdDataLoader _dataLoader;

        public GetSessionsBySpeakerIdQueryHandler(ISessionBySpeakerIdDataLoader dataLoader)
        {
            _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
        }

        public async Task<IEnumerable<Session>> Handle(GetSessionsBySpeakerIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _dataLoader.LoadAsync(new[] { request.Id }, cancellationToken);
        }
    }
}