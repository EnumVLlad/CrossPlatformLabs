namespace ClassLib;

public static class FirstLab
{
    public static void Execute(string inputFilePath, string outputFilePath)
    {
        int sum;
        int digitsCount;
        try
        {
            (sum, digitsCount) = ReadNumbersFromFile(inputFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while reading file: {e.Message}");
            return;
        }

        string max;
        string min;
        try
        {
            max = FindMaxForSumAndDigitsCount(sum, digitsCount);
            min = FindMinForSumAndDigitsCount(sum, digitsCount);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while finding numbers: {e.Message}");
            return;
        }

        try
        {
            WriteResultToFile(max, min, outputFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while writing file: {e.Message}");
        }
    }

    private static (int Sum, int DigitsCount) ReadNumbersFromFile(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new InputException($"File {inputFilePath} was not found.");
        }

        var lines = File.ReadAllLines(inputFilePath)
            .Select(static line => line.Trim())
            .Where(static line => !string.IsNullOrWhiteSpace(line))
            .ToArray();
        if (lines.Length == 0)
        {
            throw new InputException("File does not contain any text");
        }

        if (lines.Length != 1)
        {
            throw new InputException("File contains more than one line with data");
        }

        var parts = lines[0]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            throw new InputException("File contains more than two numbers");
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

    private static void WriteResultToFile(string max, string min, string outputFilePath)
    {
        File.WriteAllText(outputFilePath, $"{max} {min}");
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

