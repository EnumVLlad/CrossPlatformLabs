namespace App;

public static class Solver
{
    private const int MinNumberOfNumbers = 2;
    private const int MaxNumberOfNumbers = 1_000;
    private const int MinNumberValue = 1;
    private const int MaxNumberValue = 1_000_000_000;

    public static int Solve(int count, List<int> numbers)
    {
        ArgumentNullException.ThrowIfNull(numbers);
        if (count is < MinNumberOfNumbers or > MaxNumberOfNumbers)
        {
            throw new ArgumentException($"Кількість чисел має бути в межах від {MinNumberOfNumbers} до {MaxNumberOfNumbers}");
        }

        if (numbers.Count != count)
        {
            throw new ArgumentException("Кількість чисел не відповідає зазначеній кількості");
        }

        if (numbers.Any(static num => num is < MinNumberValue or > MaxNumberValue))
        {
            throw new ArgumentException($"Числа мають бути в межах від {MinNumberValue} до {MaxNumberValue}");
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
}
