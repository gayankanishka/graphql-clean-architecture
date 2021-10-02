using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.AddSession
{
    public class AddSessionCommandHandler : IRequestHandler<AddSessionCommand, Session>
    {
        private readonly ISessionRepository _repository;

        public AddSessionCommandHandler(ISessionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Session> Handle(AddSessionCommand request, CancellationToken cancellationToken)
        {
            var session = new Session
            {
                Title = request.Title,
                Abstract = request.Abstract,
            };

            foreach (int speakerId in request.SpeakerIds)
            {
                session.SessionSpeakers.Add(new SessionSpeaker
                {
                    SpeakerId = speakerId
                });
            }

            await _repository.AddSessionAsync(session, cancellationToken);

            return session;
        }
    }
}

