using ConferencePlanner.Domain.Entities;
using GreenDonut;

namespace ConferencePlanner.Application.Common.Interfaces
{
    public interface ISessionBySpeakerIdDataLoader : IDataLoader<int, Session>
    {
    }
}