using Dunet;
using System.Diagnostics;
using System.Numerics;

var input = File.ReadAllText("input.txt");
var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
List<List<char>>? rawMap = input
    .Split("\n", options)
    .Select(x => x.ToList())
    .ToList();

var guard = '^';
var mapWidth = rawMap[0].Count;
var mapHeight = rawMap.Count;

(int x, int y) location = (
    rawMap.First(l => l.Contains(guard)).IndexOf(guard),
    rawMap.IndexOf(rawMap.First(l => l.Contains(guard)))
);

var map = new Map(rawMap, location);

int number = 0;
int maxLoops = mapWidth * mapHeight * 2;

for(int y = 0; y < mapHeight; y++)
{
    for (int x = 0; x < mapWidth; x++)
    {
        if (location == (x, y) || map.IsObstacle((x, y), false))
            continue;

        Console.WriteLine($"({x:000}, {y:000}) out of ({mapWidth:000}, {mapHeight:000})");
        Direction direction = new Direction.Up();
        map.TemporaryObstacle = (x, y);
        map.Location = location;
        map.ResetPositions();

        for (int loops = 0; loops <= maxLoops; loops++)
        {
            try
            {
                //Console.WriteLine($"\t({map.Location.x}, {map.Location.y})");
                if (map.TryGo(direction))
                {
                }
                else if (map.IsGoingOffTheMap(direction))
                {
                    break;
                }
                else
                {
                    direction = direction.Next();
                }

                if (loops == maxLoops)
                    number++;
            }
            catch (ArgumentOutOfRangeException)
            {
                break;
            }
        }
    }
}

Console.WriteLine($"Possible locations: {number}");

[Union]
partial record Direction
{
    partial record Up(int X = 0, int Y = -1);
    partial record Right(int X = 1, int Y = 0);
    partial record Down(int X = 0, int Y = 1);
    partial record Left(int X = -1, int Y = 0);

    public Direction Next() => this switch
    {
        Direction.Up => new Direction.Right(),
        Direction.Right => new Direction.Down(),
        Direction.Down => new Direction.Left(),
        Direction.Left => new Direction.Up(),
        _ => throw new UnreachableException(),
    };

    public void Deconstruct(out int x, out int y)
    {
        var (a, b) = this switch
        {
            Direction.Up d => (d.X, d.Y),
            Direction.Right d => (d.X, d.Y),
            Direction.Down d => (d.X, d.Y),
            Direction.Left d => (d.X, d.Y),
            _ => throw new UnreachableException(),
        };

        x = a;
        y = b;
    }
}

internal class Map
{
    public Map(List<List<char>> rawMap, (int x, int y) location)
    {
        RawMap = rawMap;
        Location = location;
    }

    public List<List<char>> RawMap { get; }
    public (int x, int y) Location { get; set; }
    public (int x, int y)? TemporaryObstacle { get; set; }

    public bool TryGo(Direction direction)
    {
        var (x, y) = direction;
        var newLocation = (Location.x + x, Location.y + y);

        if (IsObstacle(newLocation))
            return false;

        if (IsGoingOffTheMap(direction))
            return false;

        RawMap[Location.y][Location.x] = 'X';

        Location = newLocation;

        RawMap[Location.y][Location.x] = 'X';
        return true;
    }

    public bool IsObstacle((int, int) location, bool includeTemporary = true)
    {
        if (includeTemporary && location == TemporaryObstacle)
            return true;

        var (x, y) = location;

        var cell = RawMap[y][x];

        return cell == '#';
    }

    public bool IsGoingOffTheMap(Direction direction)
    {
        var (x, y) = direction;
        (x, y) = (Location.x + x, Location.y + y);

        return x < 0 || y < 0 || RawMap.Count <= y || RawMap[y].Count <= x;
    }

    public int CountPositions() => RawMap.SelectMany(x => x).Count(x => x == 'X');

    internal void ResetPositions()
    {
        for (int y = 0; y < RawMap.Count; y++)
        {
            for (int x = 0; x < RawMap[y].Count; x++)
            {
                if (RawMap[y][x] == 'X')
                {
                    RawMap[y][x] = '.';
                }
            }
        }
    }
}
