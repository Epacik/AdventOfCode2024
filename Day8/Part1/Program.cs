using System.Collections.Generic;

var input = File.ReadAllText("input.txt");
var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

var uniqueFrequencies = input
    .Where(x => x != '.' && x != '\n')
    .Distinct()
    .ToArray();

var map = input.Split('\n', options)
    .Select(x => x.ToArray())
    .ToArray();

List<(int x, int y)> antinodes = [];
List<(int x, int y)> allAntennas = [];

foreach (var frequency in uniqueFrequencies)
{
    var antennas = GetAntennas(map, frequency)
        .ToArray();

    allAntennas.AddRange(antennas);

    var combinations = antennas
        .SelectMany(x => antennas, (left, right) => (left, right))
        .Where(x => x.left != x.right)
        .DistinctBy(x =>
        {
            int[] arr = [x.left.x, x.left.y, x.right.x, x.right.y];
            Array.Sort(arr);
            return (arr[0], arr[1], arr[2], arr[3]);
        })
        .ToArray();


    foreach (var (left, right) in combinations)
    {
        var x1 = ((2 * right.x) - left.x);
        var y1 = ((2 * right.y) - left.y);
        antinodes.Add((x1, y1));

        var x2 = ((2 * left.x) - right.x);
        var y2 = ((2 * left.y) - right.y);
        antinodes.Add((x2, y2));
    }
}

var count = antinodes
    .Distinct()
    .Where(x => x.x >= 0 && x.y >= 0)
    .Where(x => x.x < map[0].Length && x.y < map.Length)
    .Count();

Console.WriteLine($"Unique count: {count}");

IEnumerable<(int x, int y)> GetAntennas(char[][] map, char frequency)
{
    for(int y = 0; y < map.Length; y++)
    {
        for(int x = 0; x < map[y].Length; x++)
        {
            if (map[y][x] == frequency)
                yield return (x, y);
        }
    }
}
