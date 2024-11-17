using System.Globalization;

namespace ClassLib;

public static class SecondLab
{
    private const int MinNumberOfNumbers = 2;
    private const int MaxNumberOfNumbers = 1_000;
    private const int MinNumberValue = 1;
    private const int MaxNumberValue = 1_000_000_000;

    public static string Execute(string inputFilePath)
    {
        int count;
        List<int> numbers;
        try
        {
            (count, numbers) = ReadNumbersFromText(inputFilePath);
        }
        catch (Exception e)
        {
            return $"Error while reading file: {e.Message}";
        }

        int solution;
        try
        {
            solution = Solve(count, numbers);
            return solution.ToString(CultureInfo.InvariantCulture);
        }
        catch (Exception e)
        {
            return $"Error while solving task: {e.Message}";
        }
    }

    private static int Solve(int count, List<int> numbers)
    {
        ArgumentNullException.ThrowIfNull(numbers);
        if (count is < MinNumberOfNumbers or > MaxNumberOfNumbers)
        {
            throw new ArgumentException($"Number of numbers should be between {MinNumberOfNumbers} and {MaxNumberOfNumbers}");
        }

        if (numbers.Count != count)
        {
            throw new ArgumentException("Number of numbers does not match the count");
        }

        if (numbers.Any(static num => num is < MinNumberValue or > MaxNumberValue))
        {
            throw new ArgumentException($"All numbers should be between {MinNumberValue} and {MaxNumberValue}");
        }

        numbers.Sort();
        var minMaxes = new int[count];
        minMaxes[1] = numbers[1] - numbers[0];
        for (var i = 2; i < count; i++)
        {
            minMaxes[i] = numbers[i] - numbers[0];
            for (var j = 2; j < i; j++)
            {
                minMaxes[i] = Math.Min(minMaxes[i], Math.Max(minMaxes[j - 1], numbers[i] - numbers[j]));
            }
        }

        return minMaxes[^1];
    }

    private static (int Count, List<int> Numbers) ReadNumbersFromText(string text)
    {
        var lines = text.Replace("\r", "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(static line => line.Trim())
            .Where(static line => !string.IsNullOrWhiteSpace(line))
            .ToArray();
        if (lines.Length == 0)
        {
            throw new InputException("Input does not contain any text");
        }

        if (lines.Length != 2)
        {
            throw new InputException("Input contains more than two lines with data");
        }

        if (!int.TryParse(lines[0].Trim(), out var count))
        {
            throw new InputException("First line must contain a number");
        }

        var parts = lines[1]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var nums = new List<int>();
        foreach (var part in parts)
        {
            if (!int.TryParse(part, out var num))
            {
                throw new InputException("All numbers in the second line must be integers");
            }

            nums.Add(num);
        }

        return (count, nums);
    }
}
