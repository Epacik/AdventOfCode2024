var input = File.ReadAllText("input.txt");

var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);


int count = 0;
int horizontal = 0;
int vertical = 0;
int diagonal1 = 0;
int diagonal2 = 0;


// horizontal
for (int x = 0; x < lines.Length; x++)
{
    for (int y = 0; y < lines[x].Length - 3; y++)
    {
        char c1 = lines[x][y];
        char c2 = lines[x][y + 1];
        char c3 = lines[x][y + 2];
        char c4 = lines[x][y + 3];

        var str = new string([c1, c2, c3, c4]);

        if (str.Equals("XMAS") || str.Equals("SAMX"))
        {
            count++;
            horizontal++;
        }
    }
}

// vertical
for (int x = 0; x < lines.Length - 3; x++)
{
    for (int y = 0; y < lines[x].Length; y++)
    {
        char c1 = lines[x][y];
        char c2 = lines[x + 1][y];
        char c3 = lines[x + 2][y];
        char c4 = lines[x + 3][y];

        var str = new string([c1, c2, c3, c4]);

        if (str.Equals("XMAS") || str.Equals("SAMX"))
        {
            count++;
            vertical++;
        }
    }
}

// diagonal

for (int x = 0; x < lines.Length - 3; x++)
{
    for (int y = 0; y < lines[x].Length - 3; y++)
    {
        char c1 = lines[x][y];
        char c2 = lines[x + 1][y + 1];
        char c3 = lines[x + 2][y + 2];
        char c4 = lines[x + 3][y + 3];

        var str = new string([c1, c2, c3, c4]);

        if (str.Equals("XMAS") || str.Equals("SAMX"))
        {
            count++;
            diagonal1++;
        }

        c1 = lines[x][y + 3];
        c2 = lines[x + 1][y + 2];
        c3 = lines[x + 2][y + 1];
        c4 = lines[x + 3][y];

        str = new string([c1, c2, c3, c4]);

        if (str.Equals("XMAS") || str.Equals("SAMX"))
        {
            count++;
            diagonal2++;
        }
    }
}


Console.WriteLine($"Number: {count}");