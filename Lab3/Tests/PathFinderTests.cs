using App;
using Xunit.Abstractions;

namespace Tests;

public class PathFinderTests
{
    private readonly ITestOutputHelper _output;

    public PathFinderTests(ITestOutputHelper output) => _output = output;

    [Fact]
    public void FindPathBack_ThrowsArgumentException_WhenPathIsEmpty()
    {
        // Arrange
        var path = string.Empty;

        // Act
        Action act = () => PathFinder.FindPathBack(path);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(FindPathBack_ThrowsArgumentException_WhenPathIsEmpty)} - passed");
    }

    [Theory]
    [InlineData(201)]
    [InlineData(1_000)]
    public void FindPathBack_ThrowsArgumentException_WhenPathIsTooLong(int length)
    {
        // Arrange
        var path = new string('N', length);

        // Act
        Action act = () => PathFinder.FindPathBack(path);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(FindPathBack_ThrowsArgumentException_WhenPathIsTooLong)}: {length} - passed");
    }

    [Theory]
    [InlineData(' ')]
    [InlineData('A')]
    [InlineData('!')]
    public void FindPathBack_ThrowsArgumentException_WhenPathContainsInvalidCharacters(char invalidCharacter)
    {
        // Arrange
        var path = "N" + invalidCharacter.ToString() + "E";

        // Act
        Action act = () => PathFinder.FindPathBack(path);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(FindPathBack_ThrowsArgumentException_WhenPathContainsInvalidCharacters)}: {invalidCharacter} - passed");
    }

    [Theory]
    [InlineData("EENNESWSSWE", "NWW")]
    [InlineData("WNWSSEENNWS", "E")]
    [InlineData("NNESENNWS", "WSS")]
    public void FindPathBack_ReturnsCorrectPath_WhenPathCanBeShorter(string path, string expected)
    {
        // Act
        var result = PathFinder.FindPathBack(path);

        // Assert
        Assert.Equal(expected, result);
        _output.WriteLine($"{nameof(FindPathBack_ReturnsCorrectPath_WhenPathCanBeShorter)}: {path} => {expected} - passed");
    }

    [Theory]
    [InlineData("EESWSWN", "SENENWW")]
    [InlineData("NENENE", "WSWSWS")]
    [InlineData("NNNNN", "SSSSS")]
    public void FindPathBack_ReturnsCorrectPath_WhenPathCanOnlyBeReverse(string path, string expected)
    {
        // Act
        var result = PathFinder.FindPathBack(path);

        // Assert
        Assert.Equal(expected, result);
        _output.WriteLine($"{nameof(FindPathBack_ReturnsCorrectPath_WhenPathCanOnlyBeReverse)}: {path} => {expected} - passed");
    }
}
