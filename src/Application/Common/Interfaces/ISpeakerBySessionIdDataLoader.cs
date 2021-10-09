using ConferencePlanner.Domain.Entities;
using GreenDonut;

namespace ConferencePlanner.Application.Common.Interfaces
{
    public interface ISpeakerBySessionIdDataLoader : IDataLoader<int, Speaker>
    {
    }
}