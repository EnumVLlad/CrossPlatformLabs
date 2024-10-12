using System.Text;
using App;

Console.OutputEncoding = Encoding.Unicode;

int sum;
int digitsCount;
try
{
    (sum, digitsCount) = Handler.ReadNumbersFromFile();
}
catch (Exception e)
{
    Console.WriteLine($"Під час зчитування файлу виникла помилка: {e.Message}");
    return;
}

string max;
string min;
try
{
    max = Service.FindMaxForSum(sum, digitsCount);
    min = Service.FindMinForSum(sum, digitsCount);
}
catch (Exception e)
{
    Console.WriteLine($"Під час знаходження розв'язку виникла помилка: {e.Message}");
    return;
}

try
{
    Handler.WriteResultToFile(max, min);
}
catch (Exception e)
{
    Console.WriteLine($"Під час запису файлу виникла помилка: {e.Message}");
}
