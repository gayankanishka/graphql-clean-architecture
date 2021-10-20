using ConferencePlanner.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConferencePlanner.Infrastructure.Persistence.Imports;

public class DataImporter
{
    public async Task LoadDataAsync(ApplicationDbContext db)
    {
        await using var stream = File.OpenRead("NDC_London_2019.json");
        using var reader = new JsonTextReader(new StreamReader(stream));

        var conference = await JArray.LoadAsync(reader);
        var speakers = new Dictionary<string, Speaker>();

        foreach (var conferenceDay in conference)
        foreach (var roomData in conferenceDay["rooms"]!)
        {
            var track = new Track
            {
                Name = roomData["name"]!.ToString()
            };

            foreach (var sessionData in roomData["sessions"]!)
            {
                var session = new Session
                {
                    Title = sessionData["title"]!.ToString(),
                    Abstract = sessionData["description"]!.ToString(),
                    StartTime = sessionData["startsAt"]!.Value<DateTime>().ToUniversalTime(),
                    EndTime = sessionData["endsAt"]!.Value<DateTime>().ToUniversalTime()
                };

                track.Sessions.Add(session);

                foreach (var speakerData in sessionData["speakers"]!)
                {
                    var id = speakerData["id"]!.ToString();

                    if (!speakers.TryGetValue(id, out var speaker))
                    {
                        speaker = new Speaker
                        {
                            Name = speakerData["name"]!.ToString()
                        };

                        speakers.Add(id, speaker);
                        db.Speakers.Add(speaker);
                    }

                    session.SessionSpeakers.Add(new SessionSpeaker
                    {
                        Speaker = speaker,
                        Session = session
                    });
                }
            }

            db.Tracks.Add(track);
        }

        await db.SaveChangesAsync();
    }
}