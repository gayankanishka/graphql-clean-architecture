using ConferencePlanner.Application.Sessions.Queries.GetSessionsBySpeakerId;
using ConferencePlanner.Application.Speakers.Queries.GetSpeakerById;
using ConferencePlanner.Domain.Entities;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using MediatR;

namespace ConferencePlanner.GraphQL.Nodes
{
    [Node]
    [ExtendObjectType(typeof(Speaker))]
    public class SpeakerNode
    {
        [BindMember(nameof(Speaker.SessionSpeakers), Replace = true)]
        public async Task<IEnumerable<Session>> GetSessionsAsync(
            [Parent] Speaker speaker,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => await mediator.Send(new GetSessionsBySpeakerIdQuery(speaker.Id), cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsExpensiveAsync(
            [Parent] Speaker speaker,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
        {
            await Task.Delay(new Random().Next(1000, 3000), cancellationToken);

            return await mediator.Send(new GetSessionsBySpeakerIdQuery(speaker.Id), cancellationToken);
        }

        // public async IAsyncEnumerable<Session> GetSessionsStreamAsync(
        //     [Parent] Speaker speaker,
        //     [Service] IMediator mediator,
        //     [EnumeratorCancellation] CancellationToken cancellationToken)
        // {
        //     var random = new Random();
        //
        //     await Task.Delay(random.Next(500, 1000), cancellationToken);
        //
        //     await using var context = contextFactory.CreateDbContext();
        //
        //     var stream = (IAsyncEnumerable<SessionSpeaker>)context.Speakers
        //         .Where(s => s.Id == speaker.Id)
        //         .Include(s => s.SessionSpeakers)
        //         .SelectMany(s => s.SessionSpeakers)
        //         .Include(s => s.Session);
        //
        //     await foreach (var item in stream.WithCancellation(cancellationToken))
        //     {
        //         if (item.Session is not null)
        //         {
        //             yield return item.Session;
        //         }
        //
        //         await Task.Delay(random.Next(100, 300), cancellationToken);
        //     }
        // }
        
        [NodeResolver]
        public static Task<Speaker> GetSpeakerByIdAsync(
            GetSpeakerByIdQuery input,
            [Service] IMediator mediator,
            CancellationToken cancellationToken)
            => mediator.Send(input, cancellationToken);
    }
}