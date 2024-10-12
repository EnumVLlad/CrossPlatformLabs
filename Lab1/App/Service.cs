namespace App;

public static class Service
{
    public static string FindMaxForSum(int sum, int digitsCount)
    {
        if (sum < 1)
        {
            throw new ArgumentException("Сума не може бути менше одного", nameof(sum));
        }

        if (digitsCount < 1)
        {
            throw new ArgumentException("Кількість цифр не може бути менше одного", nameof(digitsCount));
        }

        if (sum > 9 * digitsCount)
        {
            throw new ArgumentException("Сума не може бути більше 9 * кількість цифр", nameof(sum));
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

    public static string FindMinForSum(int sum, int digitsCount)
    {
        if (sum < 1)
        {
            throw new ArgumentException("Сума не може бути менше одного", nameof(sum));
        }

        if (digitsCount < 1)
        {
            throw new ArgumentException("Кількість цифр не може бути менше одного", nameof(digitsCount));
        }

        if (sum > 9 * digitsCount)
        {
            throw new ArgumentException("Сума не може бути більше 9 * кількість цифр", nameof(sum));
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
