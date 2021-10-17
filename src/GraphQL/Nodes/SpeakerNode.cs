using System.Runtime.CompilerServices;
using ConferencePlanner.Application.Sessions.Queries.GetSessionsBySpeakerId;
using ConferencePlanner.Application.Speakers.Queries.GetSessionSpeakers;
using ConferencePlanner.Application.Speakers.Queries.GetSpeakerById;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(Speaker))]
public class SpeakerNode
{
    [BindMember(nameof(Speaker.SessionSpeakers), Replace = true)]
    public async Task<IEnumerable<Session>> GetSessionsAsync(
        [Parent] Speaker speaker,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetSessionsBySpeakerIdQuery(speaker.Id), cancellationToken);
    }

    public async Task<IEnumerable<Session>> GetSessionsExpensiveAsync(
        [Parent] Speaker speaker,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        await Task.Delay(new Random().Next(1000, 3000), cancellationToken);

        return await mediator.Send(new GetSessionsBySpeakerIdQuery(speaker.Id), cancellationToken);
    }

    public async IAsyncEnumerable<Session> GetSessionsStreamAsync(
        [Parent] Speaker speaker,
        [Service] IMediator mediator,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        var random = new Random();
        await Task.Delay(random.Next(500, 1000), cancellationToken);

        var sessionSpeakers =
            await mediator.Send(new GetSessionSpeakersQuery(speaker.Id), cancellationToken);

        var stream = (IAsyncEnumerable<SessionSpeaker>)sessionSpeakers;

        await foreach (var item in stream.WithCancellation(cancellationToken))
        {
            if (item.Session is not null) yield return item.Session;

            await Task.Delay(random.Next(100, 300), cancellationToken);
        }
    }

    [NodeResolver]
    public static Task<Speaker> GetSpeakerByIdAsync(
        GetSpeakerByIdQuery input,
        [Service] IMediator mediator,
        CancellationToken cancellationToken)
    {
        return mediator.Send(input, cancellationToken);
    }
}