using System.Text;
using App;

Console.OutputEncoding = Encoding.Unicode;

int count;
List<int> numbers;
try
{
    (count, numbers) = Handler.ReadNumbersFromFile();
}
catch (Exception e)
{
    Console.WriteLine($"Під час зчитування файлу виникла помилка: {e.Message}");
    return;
}

int solution;
try
{
    solution = Solver.Solve(count, numbers);
}
catch (Exception e)
{
    Console.WriteLine($"Під час знаходження розв'язку виникла помилка: {e.Message}");
    return;
}

try
{
    Handler.WriteResultToFile(solution);
}
catch (Exception e)
{
    Console.WriteLine($"Під час запису файлу виникла помилка: {e.Message}");
}
