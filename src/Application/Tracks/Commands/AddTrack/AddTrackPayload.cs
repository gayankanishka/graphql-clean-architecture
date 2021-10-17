using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Tracks.Commands.AddTrack
{
    public class AddTrackPayload : TrackPayloadBase
    {
        public AddTrackPayload(Track track)
            : base(track)
        {
        }

        public AddTrackPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}