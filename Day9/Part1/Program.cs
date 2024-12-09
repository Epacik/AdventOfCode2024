using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt").Trim();
var memory = new List<string>();

for(int i = 0; i < input.Length; i++)
{
    var isEven = i % 2 == 0;

    var count = int.Parse(input[i].ToString());

    var value = isEven ? (i / 2).ToString() : ".";
    for (int n = 0; n < count; n++)
    {
        memory.Add(value);
    }
}

Console.WriteLine($"Memory: {string.Concat(memory)}");

var digitRegex = new Regex(@"\d");

while (memory.IndexOf(".") < memory.LastIndexOf(memory.Last(digitRegex.IsMatch)))
{
    var dotIndex = memory.IndexOf(".");
    var digIndex = memory.LastIndexOf(memory.Last(digitRegex.IsMatch));

    memory[dotIndex] = memory[digIndex];
    memory[digIndex] = ".";
}

Console.WriteLine($"Memory: {string.Concat(memory)}");

var checksum = memory
    .Select((i, p) => (i != "." ? long.Parse(i) : 0) * p)
    .Sum();

Console.WriteLine($"Checksum: {checksum}");