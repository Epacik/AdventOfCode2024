var input = File.ReadAllText("input.txt").Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

int safeCount = 0;

for (int i = 0; i < input.Length; i++)
{
    var line = input[i];
    var report = line
        .Split(' ')
        .Select(int.Parse)
        .ToArray();

    bool isDec = report[1] < report[0];
    bool isInc = report[0] < report[1];
    bool isErr = report[0] == report[1];

    if (isErr)
        continue;

    for (int j = 0; j < report.Length - 1; j++)
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

    if (!isErr)
        safeCount++;
}

Console.WriteLine($"Safe: {safeCount}");