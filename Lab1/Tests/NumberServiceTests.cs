using App;
using Xunit.Abstractions;

namespace Tests;

public class NumberServiceTests
{
    private readonly ITestOutputHelper _output;

    public NumberServiceTests(ITestOutputHelper output) => _output = output;

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(0, 0)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    [InlineData(-1, -1)]
    public void FindMaxForSumWhenCountIsLessThanOne(int sum, int digitsCount)
    {
        // Act
        Action act = () => Service.FindMaxForSum(sum, digitsCount);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(FindMaxForSumWhenCountIsLessThanOne)}: {sum}, {digitsCount} - passed");
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(19, 2)]
    [InlineData(20, 2)]
    public void FindMaxForSumWhenSumIsGreaterThanNineTimes(int sum, int digitsCount)
    {
        // Act
        Action act = () => Service.FindMaxForSum(sum, digitsCount);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(FindMaxForSumWhenSumIsGreaterThanNineTimes)}: {sum}, {digitsCount} - passed");
    }

    [Theory]
    [InlineData(1, 1, "1")]
    [InlineData(9, 1, "9")]
    [InlineData(10, 4, "9100")]
    [InlineData(11, 4, "9200")]
    public void FindMaxForSumReturnsExpectedResult(int sum, int digitsCount, string expected)
    {
        // Act
        var actual = Service.FindMaxForSum(sum, digitsCount);

        // Assert
        Assert.Equal(expected, actual);
        _output.WriteLine($"{nameof(FindMaxForSumReturnsExpectedResult)}: {sum}, {digitsCount}, {expected} - passed");
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(0, 0)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    [InlineData(-1, -1)]
    public void FindMinForSumWhenCountIsLessThanOne(int sum, int digitsCount)
    {
        // Act
        Action act = () => Service.FindMinForSum(sum, digitsCount);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(FindMinForSumWhenCountIsLessThanOne)}: {sum}, {digitsCount} - passed");
    }

    [Theory]
    [InlineData(10, 1)]
    [InlineData(19, 2)]
    [InlineData(20, 2)]
    public void FindMinForSumWhenSumIsGreaterThanNineTimes(int sum, int digitsCount)
    {
        // Act
        Action act = () => Service.FindMinForSum(sum, digitsCount);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(FindMinForSumWhenSumIsGreaterThanNineTimes)}: {sum}, {digitsCount} - passed");
    }

    [Theory]
    [InlineData(1, 1, "1")]
    [InlineData(9, 1, "9")]
    [InlineData(10, 4, "1009")]
    [InlineData(11, 4, "1019")]
    public void FindMinForSumReturnsExpectedResult(int sum, int digitsCount, string expected)
    {
        // Act
        var actual = Service.FindMinForSum(sum, digitsCount);

        // Assert
        Assert.Equal(expected, actual);
        _output.WriteLine($"{nameof(FindMinForSumReturnsExpectedResult)}: {sum}, {digitsCount}, {expected} - passed");
    }
}
