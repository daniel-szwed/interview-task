using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InterviewTask.Models;

namespace InterviewTask.Services;

class MapProvider : IMapProvider
{
    public long GetValueForKey(string fileName, string mapHeader, long key)
    {
        var mappings = LoadMapFromFile(fileName, mapHeader);

        var map = mappings
            .FirstOrDefault(map =>
                key >= map.SourceRangeStart
                && key <= map.SourceRangeEnd);

        if (map is null)
        {
            return key;
        }
        
        var delta = map.SourceRangeStart - map.DestinationRangeStart;

        return key - delta;
    }
    
    private IEnumerable<CustomMapping> LoadMapFromFile(string fileName, string mapHeader)
    {
        string filePath = $"{fileName}";
        if (File.Exists(filePath))
        {
            using StreamReader reader = new StreamReader(filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Trim() == $"{mapHeader}:")
                {
                    return GetCustomMappingUntilEmptyLineOccurs(reader);
                }
            }
        }

        return Enumerable.Empty<CustomMapping>();
    }

    private IEnumerable<CustomMapping> GetCustomMappingUntilEmptyLineOccurs(StreamReader reader)
    {
        var result = new List<CustomMapping>();
        string line;
        
        while ((line = reader.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line.Trim()))
            {
                break;
            }
            var numbers = line.Split(' ');

            if (numbers.Length != 3)
            {
                throw new Exception($"Line {line} has unexpected format.");
            }
            
            var parseResult1 = Int64.TryParse(numbers[0], out var destinationRangeStart);
            var parseResult2 = Int64.TryParse(numbers[1], out var sourceRangeStart);
            var parseResult3 = Int64.TryParse(numbers[2], out var rangeLength);

            if (!(parseResult1 && parseResult2 && parseResult3))
            {
                throw new Exception($"Line {line} should contains integer numbers only.");
            }

            var customMapping = new CustomMapping(
                destinationRangeStart,
                sourceRangeStart,
                rangeLength);
            
            result.Add(customMapping);
        }

        return result;
    }
}