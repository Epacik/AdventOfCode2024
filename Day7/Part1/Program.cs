using System.Diagnostics;
using System.Text;

var input = File.ReadAllText("input.txt");
var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

var equations = input.Split('\n', options);

Int128 sum = 0;

foreach (var equation in equations)
{
    var (res, num, _) = equation.Split(':', options);
    var result = Int128.Parse(res);
    var numbers = num.Split(' ', options)
        .Select(Int128.Parse)
        .ToArray();

    int numberOfOperators = numbers.Length - 1;

    var isValid = false;

    for (int i = 0; i < (1 << numbers.Length); i++)
    {
        var operators = Convert
            .ToString(i, 2)
            .Select(s => s.Equals('1') ? '*' : '+')
            .ToList();

        while (operators.Count < numberOfOperators)
        {
            operators.Insert(0, '+');
        }

        var sb = new StringBuilder();
        var value = numbers[0];
        sb.Append(value);

        for (int x = 0; x < numberOfOperators; x++)
        {
            var val = numbers[x + 1];
            var op = operators[x];
            value = op switch
            {
                '+' => value + val,
                '*' => value * val,
                _ => throw new UnreachableException(),
            };

            sb.Append(op);
            sb.Append(val);
        }

        if (value == result)
            isValid = true;
    }

    if (isValid)
        sum += result;
}

Console.WriteLine($"Result: {sum}");

static class Extensions
{
    public static void Deconstruct<T>(this T[] arr, out T first, out T[] rest)
    {
        first = arr[0];
        rest = arr[1..];
    }

    public static void Deconstruct<T>(this T[] arr, out T first, out T second, out T[] rest)
    {
        first = arr[0];
        second = arr[1];
        rest = arr[2..];
    }
}
