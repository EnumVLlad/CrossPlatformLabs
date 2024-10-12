using System.Globalization;

namespace App;

public static class Handler
{
    //прописати шлях до файлу тхт 
    private const string InputFileName = "Input.txt";
    private const string OutputFileName = "Output.txt";

    public static (int Count, List<int> Numbers) ReadNumbersFromFile()
    {
        if (!File.Exists(InputFileName))
        {
            throw new Input($"Файл {InputFileName} не було знайдено.");
        }

        var lines = File.ReadAllLines(InputFileName)
            .Select(static line => line.Trim())
            .Where(static line => !string.IsNullOrWhiteSpace(line))
            .ToArray();
        if (lines.Length == 0)
        {
            throw new Input("Файл не містить жодного тексту");
        }

        if (lines.Length != 2)
        {
            throw new Input("Файл містить більше двох рядків з даними");
        }

        if (!int.TryParse(lines[0].Trim(), out var count))
        {
            throw new Input("Перше число має бути цілим числом");
        }

        var parts = lines[1]
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var nums = new List<int>();
        foreach (var part in parts)
        {
            if (!int.TryParse(part, out var num))
            {
                throw new Input("Другий рядок має містити тільки цілі числа, розділені пробілами");
            }

            nums.Add(num);
        }

        return (count, nums);
    }

    public static void WriteResultToFile(int num)
    {
        File.WriteAllText(OutputFileName, num.ToString(CultureInfo.InvariantCulture));
    }
}
