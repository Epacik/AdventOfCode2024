using Dunet;
using System.Diagnostics;

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

Direction direction = new Direction.Up();

int number = 0;

while (true)
{
    try
    {
        if (map.TryGo(direction))
        {
            number++;
        }
        else
        {
            direction = direction.Next();
        }

        if (map.IsGoingOffTheMap(direction))
            break;
    }
    catch (ArgumentOutOfRangeException)
    {
        break;
    }
}

Console.WriteLine($"visited Locations: {map.CountPositions()}");

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
    public (int x, int y) Location { get; private set; }

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

    private bool IsObstacle((int, int) location)
    {
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
}
