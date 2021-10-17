using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Tracks.Commands.RenameTrack
{
    public class RenameTrackPayload : TrackPayloadBase
    {
        public RenameTrackPayload(Track track)
            : base(track)
        {
        }

        public RenameTrackPayload(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }
    }
}