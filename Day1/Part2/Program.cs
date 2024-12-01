var input = File.ReadAllText("input.txt");
var leftList = new List<int>();
var rightList = new List<int>();

foreach (var line in input.Split('\n'))
{
    var nums = line.Split("   ");
    if (nums.Length < 2)
        continue;

    leftList.Add(int.Parse(nums[0]));
    rightList.Add(int.Parse(nums[1]));
}

int score = 0;
foreach (var left in leftList)
{
    var rightCount = rightList.Count(x => x == left);

    score += (rightCount * left);
}

Console.WriteLine($"Score is {score}");