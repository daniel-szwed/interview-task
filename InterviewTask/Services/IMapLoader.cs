using InterviewTask.Models;

namespace InterviewTask.Services;

public interface IMapLoader
{
    IEnumerable<CustomMapping> LoadMapFromFile(string fileName, string mapHeader);
}