namespace App;

public static class Handler
{
    //прописати шлях до файлів
    private const string InputFileName = "Input.txt";
    private const string OutputFileName = "Output.txt";

    public static string ReadPathFromFile()
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

        if (lines.Length != 1)
        {
            throw new Input("Файл містить більше одного рядка з даними");
        }

        return lines[0].Trim();
    }

    public static void WriteResultToFile(string path)
    {
        File.WriteAllText(OutputFileName, path);
    }
}
