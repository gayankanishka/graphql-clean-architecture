using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Speakers;

public class SpeakerPayloadBase : Payload
{
    protected SpeakerPayloadBase(Speaker speaker)
    {
        Speaker = speaker;
    }

    protected SpeakerPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }

    public Speaker? Speaker { get; }
}