using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");
var regex = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");

MatchCollection? matches = regex.Matches(input);

Console.WriteLine();
var sum = 0;
foreach (var match in matches as IEnumerable<Match>)
{
    if (match.Groups.Count != 3)
        continue;

    var x = int.Parse(match.Groups[1].Value);
    var y = int.Parse(match.Groups[2].Value);
    sum += x * y;
}

Console.WriteLine($"Sum: {sum}");