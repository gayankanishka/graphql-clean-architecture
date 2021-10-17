using ConferencePlanner.Application.Speakers.Queries.GetSpeakersBySessionId;
using ConferencePlanner.Application.Tracks.Queries.GetTrackById;
using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.ScheduleSession;

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
        if (Session is null) return null;

        return await mediator.Send(new GetTrackByIdQuery(Session.Id), cancellationToken);
    }

    public async Task<IEnumerable<Speaker>?> GetSpeakersAsync(
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        if (Session is null) return null;

        return await mediator.Send(new GetSpeakersBySessionIdQuery(Session.Id), cancellationToken);
    }
}