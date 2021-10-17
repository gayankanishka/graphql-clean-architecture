using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetCheckInCount
{
    public record GetCheckInCountQuery(
        [property: ID(nameof(Session))] int Id) : IRequest<int>;
}