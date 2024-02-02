using InterviewTask.Models;
using InterviewTask.Services;

namespace InterviewTask.Tests.Services;

public class MapProviderTests
{
    private readonly IMapProvider _sut;
    private readonly IMapLoader _mapLoader = Substitute.For<IMapLoader>();
    
    public MapProviderTests()
    {
        _sut = new MapProvider(_mapLoader);
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public void GetValueForKey_CustomMapping_ShiftedKey(long source, long destination)
    {
        // Arrange
        var fileName = "fileName";
        var mapHeader = "mapHeader";
        _mapLoader
            .LoadMapFromFile(Arg.Any<string>(), Arg.Any<string>())
            .Returns<IEnumerable<CustomMapping>>(_ => GetCustomMapping());
        
        // Act
        var result = _sut.GetValueForKey(fileName, mapHeader, source);
        
        // Assert
        Assert.Equal(destination, result);
    }

    // given source and expected destination
    public static IEnumerable<object[]> GetTestCases()
    {
        yield return new object[] {0, 0};
        yield return new object[] {1, 1};
        yield return new object[] {48, 48};
        yield return new object[] {49, 49};
        yield return new object[] { 50, 52 };
        yield return new object[] { 51, 53 };
        yield return new object[] { 96, 98 };
        yield return new object[] { 97, 99 };
        yield return new object[] { 98, 50 };
        yield return new object[] { 99, 51 };
    }
    
    private IEnumerable<CustomMapping> GetCustomMapping()
    {
        yield return new CustomMapping(50, 98, 2);
        yield return new CustomMapping(52, 50, 48);
    }
}