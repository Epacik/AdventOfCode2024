var input = File.ReadAllText("input.txt");
var options = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;
var sections = input.Split("\n\n", options);

var rules = new Dictionary<string, List<string>>();

foreach(var line in sections[0].Split('\n', options))
{
    var rule = line.Split('|', options);

    if (!rules.ContainsKey(rule[0]))
        rules[rule[0]] = [];

    rules[rule[0]].Add(rule[1]);
}

int sum = 0;

foreach(var line in sections[1].Split('\n', options))
{
    var nums = line.Split(',', options);

    var valid = true;

    for (int i = 0; i < nums.Length - 1; i++)
    {
        var current = nums[i];
        for(int j = i; j < nums.Length; j++)
        {
            if (rules.TryGetValue(nums[j], out var rule) && rule.Contains(current))
                valid = false;
        }
    }

    if (valid)
    {
        int index = nums.Length / 2;
        int num = int.Parse(nums[index]);
        sum += num;
    }
}


Console.WriteLine($"Sum: {sum}");