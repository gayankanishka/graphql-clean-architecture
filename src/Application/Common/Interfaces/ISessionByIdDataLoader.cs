using ConferencePlanner.Domain.Entities;
using GreenDonut;

namespace ConferencePlanner.Application.Common.Interfaces
{
    public interface ISessionByIdDataLoader : IDataLoader<int, Session>
    {
    }
}