using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Commands.AddSpeaker
{
    public class AddSpeakerCommandHandler : IRequestHandler<AddSpeakerCommand, Speaker>
    {
        private readonly ISpeakerRepository _repository;

        public AddSpeakerCommandHandler(ISpeakerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Speaker> Handle(AddSpeakerCommand request, CancellationToken cancellationToken)
        {
            var speaker = new Speaker
            {
                Name = request.Name,
                Bio = request.Bio,
                WebSite = request.WebSite
            };

            await _repository.AddSpeakerAsync(speaker, cancellationToken);
            return speaker;
        }
    }
}

