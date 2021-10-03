using ConferencePlanner.Domain.Entities;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Queries.GetSpeakerById
{
    public record GetSpeakerByIdQuery([ID(nameof(Speaker))] int Id) : IRequest<Speaker>;
}

