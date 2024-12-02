var input = File.ReadAllText("input.txt").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

int safeCount = 0;

for (int i = 0; i < input.Length; i++)
{
    var currentCount = safeCount;
    var line = input[i];
    var report = line
        .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(int.Parse)
        .ToList();
    
    if (IsSafe(report))
    {
        safeCount++;
        continue;
    }
    
    Console.Write("");

    var count = report.Count;

    for (int j = 0; j < count; j++)
    {
        report = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse)
            .ToList();

        report.RemoveAt(j);

        if (IsSafe(report))
        {
            safeCount++;
            break;
        }
    }

    if (safeCount == currentCount)
        Console.WriteLine($"""
            UNSAFE: [{line}]
            """);
}

Console.WriteLine($"Safe: {safeCount}");

bool IsSafe(List<int> report)
{
    bool isDec = report[1] < report[0];
    bool isInc = report[0] < report[1];
    bool isErr = report[0] == report[1];

    if (isErr)
        return false;

    for (int j = 0; j < report.Count - 1; j++)
    {
        if (isErr)
            break;

        var diff = Math.Abs(report[j] - report[j + 1]);

        if ((isDec && report[j] <= report[j + 1]) || (isInc && report[j] >= report[j + 1]) || diff == 0 || diff > 3)
        {
            isErr = true;
            break;
        }
    }

    return !isErr;
}