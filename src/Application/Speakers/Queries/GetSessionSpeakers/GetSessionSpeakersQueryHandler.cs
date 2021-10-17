using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ConferencePlanner.Application.Speakers.Queries.GetSessionSpeakers
{
    public class GetSessionSpeakersQueryHandler : IRequestHandler<GetSessionSpeakersQuery, IEnumerable<SessionSpeaker>>
    {
        private readonly ISpeakerRepository _repository;

        public GetSessionSpeakersQueryHandler(ISpeakerRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<SessionSpeaker>> Handle(GetSessionSpeakersQuery request,
            CancellationToken cancellationToken)
        {
            return _repository.GetAllSpeakers()
                .Where(s => s.Id == request.Id)
                .Include(s => s.SessionSpeakers)
                .SelectMany(s => s.SessionSpeakers)
                .Include(s => s.Session);
        }
    }
}