using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Speakers.Commands.AddSpeaker;

public record AddSpeakerCommand(
    string Name,
    string? Bio,
    string? WebSite) : IRequest<Speaker>;