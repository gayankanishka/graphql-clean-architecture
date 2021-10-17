using ConferencePlanner.Application.Speakers;
using ConferencePlanner.Application.Speakers.Commands.AddSpeaker;
using ConferencePlanner.Application.Speakers.Commands.ModifySpeaker;
using ConferencePlanner.Domain.Common;
using HotChocolate;
using HotChocolate.Types;
using MediatR;

namespace ConferencePlanner.GraphQL.Mutations
{
    [ExtendObjectType(OperationTypeNames.Mutation)]
    public class SpeakerMutations
    {
        public async Task<AddSpeakerPayload> AddSpeakerAsync(
            AddSpeakerCommand input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            var speaker = await mediator.Send(input, cancellationToken);

            return new AddSpeakerPayload(speaker);
        }

        public async Task<ModifySpeakerPayload> ModifySpeakerAsync(
            ModifySpeakerCommand input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            if (input.Name.HasValue && input.Name.Value is null)
            {
                return new ModifySpeakerPayload(
                    new UserError("Name cannot be null", "NAME_NULL"));
            }

            var speaker = await mediator.Send(input, cancellationToken);

            if (speaker is null)
            {
                return new ModifySpeakerPayload(
                    new UserError("Speaker with id not found.", "SPEAKER_NOT_FOUND"));
            }

            return new ModifySpeakerPayload(speaker);
        }
    }
}