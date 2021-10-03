using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Speakers.Commands.AddSpeaker
{
    public class AddSpeakerPayload : SpeakerPayloadBase
    {
        public AddSpeakerPayload(Speaker speaker)
            : base(speaker)
        {
        }

        public AddSpeakerPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}