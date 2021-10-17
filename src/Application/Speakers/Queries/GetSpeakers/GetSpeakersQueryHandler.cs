using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakers
{
    public class GetSpeakersQueryHandler : IRequestHandler<GetSpeakersQuery, IQueryable<Speaker>>
    {
        private readonly ISpeakerRepository _repository;

        public GetSpeakersQueryHandler(ISpeakerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IQueryable<Speaker>> Handle(GetSpeakersQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAllSpeakers()
                .OrderBy(t => t.Name);
        }
    }
}