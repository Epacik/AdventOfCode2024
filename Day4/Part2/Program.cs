using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

int count = 0;

// horizontal
for (int x = 0; x < lines.Length - 2; x++)
{
    for (int y = 0; y < lines[x].Length - 2; y++)
    {

        var str1 = new string([
            lines[x][y],
            lines[x + 1][y + 1],
            lines[x + 2][y + 2]
        ]);


        var str2 = new string([
            lines[x][y + 2],
            lines[x + 1][y + 1],
            lines[x + 2][y]
        ]);

        if ((str1.Equals("MAS") || str1.Equals("SAM")) &&
            (str2.Equals("MAS") || str2.Equals("SAM")))
        {
            count++;
        }
    }
}


Console.WriteLine($"Number: {count}");