using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendees;

public record GetAttendeesQuery() : IRequest<IQueryable<Attendee>>;