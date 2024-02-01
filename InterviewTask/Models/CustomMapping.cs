namespace InterviewTask.Models;

public class CustomMapping
{
    public CustomMapping(long destinationRangeStart, long sourceRangeStart, long rangeLength)
    {
        DestinationRangeStart = destinationRangeStart;
        SourceRangeStart = sourceRangeStart;
        RangeLength = rangeLength;
    }

    public long DestinationRangeStart { get; set; }
    public long SourceRangeStart { get; set; }
    public long RangeLength { get; set; }
    public long DestinationRangeEnd => DestinationRangeStart + RangeLength - 1;
    public long SourceRangeEnd => SourceRangeStart + RangeLength - 1;
}