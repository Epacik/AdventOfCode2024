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
    var invalids = new List<string>();

    var allOperators = GetOperators(numberOfOperators);

    foreach(var operators in allOperators)
    {

        var sb = new StringBuilder();
        var value = numbers[0];
        sb.Append(value);

        for (int x = 0; x < numberOfOperators; x++)
        {
            var val = numbers[x + 1];
            var op = operators[x];
            value = op switch
            {
                "+" => value + val,
                "*" => value * val,
                "||" => value = Int128.Parse(value.ToString() + val.ToString()),
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

static List<List<string>> GetOperators(int number)
{
    List<List<string>> result = [];

    for (int i = 0; i < (int)Math.Pow(3, number); i++)
    {
        var operators = i.ToBase3String().Select(x => x).ToList();

        while (operators.Count < number)
        {
            operators.Insert(0, '0');
        }
        result.Add(operators.ConvertAll(x => x switch
        {
            '0' => "+",
            '1' => "*",
            '2' => "||",
            _ => throw new UnreachableException(),
        }));
    }

    return result;
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

    public static string ToString(this int value, char[] baseChars)
    {
        string result = string.Empty;
        int targetBase = baseChars.Length;

        do
        {
            result = baseChars[value % targetBase] + result;
            value = value / targetBase;
        }
        while (value > 0);

        return result;
    }

    public static string ToBase3String(this int value) => ToString(value, ['0', '1', '2']);
}