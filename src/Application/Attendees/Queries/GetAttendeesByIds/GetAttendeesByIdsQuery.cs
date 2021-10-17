using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendeesByIds;

public record GetAttendeesByIdsQuery([ID(nameof(Attendee))] int[] Ids) : IRequest<IEnumerable<Attendee>>;