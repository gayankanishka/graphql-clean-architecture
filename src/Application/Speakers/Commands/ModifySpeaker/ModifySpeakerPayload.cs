using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;
using ConferencePlanner.GraphQL.Speakers;

namespace ConferencePlanner.Application.Speakers.Commands.ModifySpeaker
{
    public class ModifySpeakerPayload : SpeakerPayloadBase
    {
        public ModifySpeakerPayload(Speaker speaker)
            : base(speaker)
        {
        }

        public ModifySpeakerPayload(UserError error)
            : base(new [] { error })
        {
        }
    }
}