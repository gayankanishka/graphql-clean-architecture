using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Tracks.Commands.AddTrack;

public record AddTrackCommand(string Name) : IRequest<Track>;