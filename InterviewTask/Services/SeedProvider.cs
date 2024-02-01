using System;
using System.IO;
using System.Linq;

namespace InterviewTask.Services;

class SeedProvider : ISeedProvider
{
    public long[] GetSeeds(string fileName, string seedHeader)
    {
        string filePath = $"{fileName}";
        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains(seedHeader))
                    {
                        return line
                            .Split(' ')
                            .Skip(1)
                            .Select(seed => Int64.Parse(seed))
                            .ToArray();
                    }
                }
            }
        }

        return Array.Empty<long>();
    }
}