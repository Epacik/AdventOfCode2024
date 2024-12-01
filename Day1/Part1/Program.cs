
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

leftList.Sort();
rightList.Sort();

var distances = new List<int>();
for (int i = 0; i < Math.Min(leftList.Count, rightList.Count); i++)
{
    var leftNum = leftList[i];
    var rightNum = rightList[i];
    distances.Add(Math.Abs(leftNum - rightNum));
}

Console.WriteLine($"Result: {distances.Sum()}");
