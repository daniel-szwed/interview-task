// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using InterviewTask;
using InterviewTask.Services;

internal class Program
{
    public static void Main(string[] args)
    {
        var fileName = "zadanie_input.txt";
        var mapLoader = new MapLoader();
        var mapProvider = new MapProvider(mapLoader);
        var seedProvider = new SeedProvider();
        var seeds = seedProvider.GetSeeds(fileName, InputFileHeaders.Seeds);
        var seedToLocation = new Dictionary<long, long>();

        foreach (var seed in seeds)
        {
            var soil = mapProvider.GetValueForKey(fileName, InputFileHeaders.SeedToSoilMap, seed);
            var fertilizer = mapProvider.GetValueForKey(fileName, InputFileHeaders.SoilToFertilizerMap, soil);
            var water = mapProvider.GetValueForKey(fileName, InputFileHeaders.FertilizerToWaterMap, fertilizer);
            var light = mapProvider.GetValueForKey(fileName, InputFileHeaders.WaterToLightMap, water);
            var temperature = mapProvider.GetValueForKey(fileName, InputFileHeaders.LightToTemperatureMap, light);
            var humidity = mapProvider.GetValueForKey(fileName, InputFileHeaders.TemperatureToHumidityMap, temperature);
            var location = mapProvider.GetValueForKey(fileName, InputFileHeaders.HumidityToLocationMap, humidity);
            
            seedToLocation.Add(seed, location);
        }

        var minimumLocation = seedToLocation.Values.Min();
        var seedCorrespondingWithMinLocation = seedToLocation
            .First(kvp => kvp.Value == minimumLocation).Key;
        Console.WriteLine($"Seed with lowest location is: {seedCorrespondingWithMinLocation}");
        Console.ReadKey();
    }
}