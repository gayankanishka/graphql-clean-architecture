using ConferencePlanner.Application.Common.Interfaces;
using ConferencePlanner.Domain.Entities;
using MediatR;

namespace ConferencePlanner.Application.Attendees.Queries.GetAttendeesByIds;

public class GetAttendeesByIdsQueryHandler : IRequestHandler<GetAttendeesByIdsQuery, IEnumerable<Attendee>>
{
    private readonly IAttendeeByIdDataLoader _dataLoader;

    public GetAttendeesByIdsQueryHandler(IAttendeeByIdDataLoader dataLoader)
    {
        _dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
    }

    public async Task<IEnumerable<Attendee>> Handle(GetAttendeesByIdsQuery request,
        CancellationToken cancellationToken)
    {
        return await _dataLoader.LoadAsync(request.Ids, cancellationToken);
    }
}