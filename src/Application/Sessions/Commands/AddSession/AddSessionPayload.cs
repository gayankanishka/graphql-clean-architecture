using ConferencePlanner.Domain.Common;
using ConferencePlanner.Domain.Entities;

namespace ConferencePlanner.Application.Sessions.Commands.AddSession;

public class AddSessionPayload : Payload
{
    public AddSessionPayload(Session session)
    {
        Session = session;
    }

    public AddSessionPayload(UserError error)
        : base(new[] { error })
    {
    }

    public Session? Session { get; }
}