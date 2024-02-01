using InterviewTask.Models;

namespace InterviewTask.Services;

public interface IMapProvider
{
    public long GetValueForKey(string fileName, string mapHeader, long key);
}