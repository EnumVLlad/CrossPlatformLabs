namespace ClassLib;

public static class FirstLab
{
    public static string Execute(string text)
    {
        int sum;
        int digitsCount;
        try
        {
            (sum, digitsCount) = ReadNumbersFromText(text);
        }
        catch (Exception e)
        {
            return $"Error while reading text: {e.Message}";
        }

        string max;
        string min;
        try
        {
            max = FindMaxForSumAndDigitsCount(sum, digitsCount);
            min = FindMinForSumAndDigitsCount(sum, digitsCount);
            return $"{max} {min}";
        }
        catch (Exception e)
        {
            return $"Error while finding numbers: {e.Message}";
        }
    }

    private static (int Sum, int DigitsCount) ReadNumbersFromText(string text)
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

        if (lines.Length != 1)
        {
            throw new InputException("Input contains more than one line with data");
        }

        var parts = lines[0]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            throw new InputException("Input contains more than two numbers");
        }

        if (!int.TryParse(parts[0], out var sum))
        {
            throw new InputException("First number must be an integer");
        }

        if (!int.TryParse(parts[1], out var digitsCount))
        {
            throw new InputException("Second number must be an integer");
        }

        return (sum, digitsCount);
    }

    private static string FindMaxForSumAndDigitsCount(int sum, int digitsCount)
    {
        if (sum < 1)
        {
            throw new ArgumentException("Sum must be greater than zero", nameof(sum));
        }

        if (digitsCount < 1)
        {
            throw new ArgumentException("Digits count must be greater than zero", nameof(digitsCount));
        }

        if (sum > 9 * digitsCount)
        {
            throw new ArgumentException("Sum must be less than 9 * digits count", nameof(sum));
        }

        var chars = new char[digitsCount];
        Array.Fill(chars, '0');
        var index = 0;
        while (sum > 0)
        {
            var digit = Math.Min(sum, 9);
            chars[index] = (char)(digit + '0');
            sum -= digit;
            index++;
        }

        return new string(chars);
    }

    private static string FindMinForSumAndDigitsCount(int sum, int digitsCount)
    {
        if (sum < 1)
        {
            throw new ArgumentException("Sum must be greater than zero", nameof(sum));
        }

        if (digitsCount < 1)
        {
            throw new ArgumentException("Digits count must be greater than zero", nameof(digitsCount));
        }

        if (sum > 9 * digitsCount)
        {
            throw new ArgumentException("Sum must be less than 9 * digits count", nameof(sum));
        }

        var chars = new char[digitsCount];
        Array.Fill(chars, '0');
        chars[0] = '1';
        var index = digitsCount - 1;
        sum--;
        while (sum > 0)
        {
            var digit = Math.Min(sum, 9);
            if (index == 0)
            {
                sum++;
                digit++;
            }

            chars[index] = (char)(digit + '0');
            sum -= digit;
            index--;
        }

        return new string(chars);
    }
}

