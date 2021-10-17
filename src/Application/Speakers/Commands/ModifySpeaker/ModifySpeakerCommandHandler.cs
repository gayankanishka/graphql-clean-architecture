using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Commands.ModifySpeaker;

public class ModifySpeakerCommandHandler : IRequestHandler<ModifySpeakerCommand, Speaker?>
{
    private readonly ISpeakerRepository _repository;

    public ModifySpeakerCommandHandler(ISpeakerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Speaker?> Handle(ModifySpeakerCommand request, CancellationToken cancellationToken)
    {
        var speaker = await _repository.FindSpeakerByIdAsync(request.Id, cancellationToken);

        if (speaker is null) return speaker;

        if (request.Name.HasValue) speaker.Name = request.Name;

        if (request.Bio.HasValue) speaker.Bio = request.Bio;

        if (request.WebSite.HasValue) speaker.WebSite = request.WebSite;

        await _repository.UpdateSpeakerAsync(speaker, cancellationToken);
        return speaker;
    }
}