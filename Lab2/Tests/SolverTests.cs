using App;
using Xunit.Abstractions;

namespace Tests;

public class SolverTests
{
    private readonly ITestOutputHelper _output;

    public SolverTests(ITestOutputHelper output) => _output = output;

    public static List<object[]> CorrectData =>
    [
        [2, new List<int> { 1, 1_000_000_000 }, 999_999_999],
        [3, new List<int> { 1, 2, 3 }, 2],
        [8, new List<int> { 1, 10, 100, 1_000, 1_000, 100, 10, 1 }, 0],
        [10, new List<int> { 258, 740, 156, 244, 458, 680, 390, 694, 844, 817 }, 102],
    ];

    [Fact]
    public void Solve_ThrowsArgumentNullException_WhenNumbersIsNull()
    {
        // Arrange
        List<int> numbers = null!;

        // Act
        Action act = () => Solver.Solve(10, numbers);

        // Assert
        _ = Assert.Throws<ArgumentNullException>(act);
        _output.WriteLine($"{nameof(Solve_ThrowsArgumentNullException_WhenNumbersIsNull)} - passed");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1_001)]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1_000_000)]
    public void Solve_ThrowsArgumentException_WhenCountOutOfRange(int count)
    {
        // Arrange
        var numbers = new List<int> { 1, 2, 3 };

        // Act
        Action act = () => Solver.Solve(count, numbers);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(Solve_ThrowsArgumentException_WhenCountOutOfRange)}: {count} - passed");
    }

    [Fact]
    public void Solve_ThrowsArgumentException_WhenCountDoesNotMatchNumbersCount()
    {
        // Arrange
        var numbers = new List<int> { 1, 2, 3 };

        // Act
        Action act = () => Solver.Solve(4, numbers);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(Solve_ThrowsArgumentException_WhenCountDoesNotMatchNumbersCount)} - passed");
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1_000_000_001)]
    public void Solve_ThrowsArgumentException_WhenNumbersContainsOutOfRangeValues(int number)
    {
        // Arrange
        var numbers = new List<int> { 1, 2, number };

        // Act
        Action act = () => Solver.Solve(3, numbers);

        // Assert
        _ = Assert.Throws<ArgumentException>(act);
        _output.WriteLine($"{nameof(Solve_ThrowsArgumentException_WhenNumbersContainsOutOfRangeValues)}: {number} - passed");
    }

    [Theory]
    [MemberData(nameof(CorrectData))]
    public void Solve_ReturnsExpectedResult(int count, List<int> numbers, int expected)
    {
        // Act
        var result = Solver.Solve(count, numbers);

        // Assert
        Assert.Equal(expected, result);
        _output.WriteLine($"{nameof(Solve_ReturnsExpectedResult)}: {result} - passed");
    }
}
