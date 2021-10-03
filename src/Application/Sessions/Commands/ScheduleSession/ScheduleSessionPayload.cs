using ConferencePlanner.Application.Tracks.Queries.GetTrackById;
using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.ScheduleSession
{
    public class ScheduleSessionPayload : SessionPayloadBase
    {
        public ScheduleSessionPayload(Session session)
            : base(session)
        {
        }

        public ScheduleSessionPayload(UserError error)
            : base(new[] { error })
        {
        }

        public async Task<Track?> GetTrackAsync(
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            if (Session is null)
            {
                return null;
            }

            return await mediator.Send(new GetTrackByIdQuery(Session.Id), cancellationToken);
        }

        [UseApplicationDbContext]
        public async Task<IEnumerable<Speaker>?> GetSpeakersAsync(
            [ScopedService] ApplicationDbContext dbContext,
            SpeakerByIdDataLoader speakerById,
            CancellationToken cancellationToken)
        {
            if (Session is null)
            {
                return null;
            }

            int[] speakerIds = await dbContext.Sessions
                .Where(s => s.Id == Session.Id)
                .Include(s => s.SessionSpeakers)
                .SelectMany(s => s.SessionSpeakers.Select(t => t.SpeakerId))
                .ToArrayAsync(cancellationToken);

            return await speakerById.LoadAsync(speakerIds, cancellationToken);
        }
    }
}