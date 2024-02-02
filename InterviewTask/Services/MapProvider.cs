using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InterviewTask.Models;

namespace InterviewTask.Services;

public class MapProvider : IMapProvider
{
    private readonly IMapLoader _mapLoader;

    public MapProvider(IMapLoader mapLoader)
    {
        _mapLoader = mapLoader;
    }
    
    public long GetValueForKey(string fileName, string mapHeader, long key)
    {
        var mappings = _mapLoader.LoadMapFromFile(fileName, mapHeader);

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
}