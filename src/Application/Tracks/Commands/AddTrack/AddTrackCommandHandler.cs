using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Commands.AddTrack
{
    public class AddTrackCommandHandler : IRequestHandler<AddTrackCommand, Track>
    {
        private readonly ITrackRepository _repository;

        public AddTrackCommandHandler(ITrackRepository repository)
        {
            _repository = repository;
        }

        public async Task<Track> Handle(AddTrackCommand request, CancellationToken cancellationToken)
        {
            var track = new Track { Name = request.Name };

            await _repository.AddTrackAsync(track, cancellationToken);
            return track;
        }
    }
}