using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Sessions;

public class SessionPayloadBase : Payload
{
    protected SessionPayloadBase(Session session)
    {
        Session = session;
    }

    protected SessionPayloadBase(IReadOnlyList<UserError> errors)
        : base(errors)
    {
    }

    public Session? Session { get; }
}