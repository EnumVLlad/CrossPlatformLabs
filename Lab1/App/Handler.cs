namespace App;

public static class Handler
{
    //Прописати шлях до файлів
    private const string InputFileName = "Input.txt";
    private const string OutputFileName = "Output.txt";

    public static (int Sum, int DigitsCount) ReadNumbersFromFile()
    {
        if (!File.Exists(InputFileName))
        {
            throw new InvalidOperationException($"Файл {InputFileName} не знайдено.");
        }

        var lines = File.ReadAllLines(InputFileName)
            .Select(static line => line.Trim())
            .Where(static line => !string.IsNullOrWhiteSpace(line))
            .ToArray();

        if (lines.Length == 0)
        {
            throw new InvalidOperationException("Файл пустий.");
        }

        if (lines.Length != 1)
        {
            throw new InvalidOperationException("Файл повинен мати лише одну строчку.");
        }

        var parts = lines[0]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
        {
            throw new InvalidOperationException("Строка повина мати лише два числа розділені пробілом.");
        }

        if (!int.TryParse(parts[0], out var sum))
        {
            throw new InvalidOperationException("Перше число повинно бути цілим.");
        }

        if (!int.TryParse(parts[1], out var digitsCount))
        {
            throw new InvalidOperationException("Друге число повинно бути цілим.");
        }

        return (sum, digitsCount);
    }

    public static void WriteResultToFile(string max, string min)
    {
        try
        {
            File.WriteAllText(OutputFileName, $"{max} {min}");
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException($"Помилка при спробі запису файду {OutputFileName}: {ex.Message}");
        }
    }
}
