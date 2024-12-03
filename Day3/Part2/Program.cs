using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");
var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)|do\(\)|don\'t\(\)");

MatchCollection? matches = regex.Matches(input);

var enabled = true;
var sum = 0;

foreach (var match in matches as IEnumerable<Match>)
{
    if (match.Value.Equals("don't()"))
    {
        enabled = false;
        continue;
    }
    if (match.Value.Equals("do()"))
    {
        enabled = true;
        continue;
    }

    if (!enabled)
        continue;

    if (match.Groups.Count != 3)
        continue;

    var x = int.Parse(match.Groups[1].Value);
    var y = int.Parse(match.Groups[2].Value);
    sum += x * y;
}

Console.WriteLine($"Sum: {sum}");