using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Sessions.Commands.ScheduleSession
{
    public record ScheduleSessionCommand(
        [property: ID(nameof(Session))]
        int SessionId,
        [property: ID(nameof(Track))]
        int TrackId,
        DateTimeOffset StartTime,
        DateTimeOffset EndTime) : IRequest<Session?>;
}