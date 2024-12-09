using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt").Trim();
//var memory = new List<(string value, int length)>();
var intMemory = new List<int>();
for (int i = 0; i < input.Length; i++)
{
    var isEven = i % 2 == 0;

    var count = int.Parse(input[i].ToString());
    if (count == 0)
        continue;

    var value = isEven ? (i / 2).ToString() : ".";
    //memory.Add((value, count));

    var intValue = isEven ? (i / 2) : -1;
    for (int n = 0; n < count; n++)
    {
        intMemory.Add(intValue);
    }
}

//Console.WriteLine($"Memory: {string.Concat(memory)}");


var highestIndex = intMemory.LastOrDefault(x => x >= 0);

var moved = new List<string>();

for (int i = highestIndex; i >= 0; i--)
{
    var firstIndex = intMemory.IndexOf(i);
    var lastIndex = intMemory.LastIndexOf(i);

    var length = lastIndex - firstIndex + 1;

}

//for (int i = memory.Count - 1; i > 0; i--)
//{
//    var item = memory[i];
//    if (item.value == ".")
//        continue;

//    if (moved.Contains(item.value))
//        continue;

//    moved.Add(item.value);

//    var space = memory.FirstOrDefault(
//        x => x.value == "." &&
//            x.length >= item.length);

//    if (space == default)
//        continue;

//    var index = memory.IndexOf(space);

//    if (i < index)
//        continue;

//    if (space.length == item.length)
//    {
//        memory[index] = memory[i];
//        memory[i] = (".", item.length);
//    }
//    else
//    {
//        var difference = space.length - item.length;
//        memory[index] = memory[i];
//        memory[i] = (".", item.length);
//        memory.Insert(index + 1, (".", difference));
//    }
    
//}

//Console.WriteLine($"Memory: {string.Concat(memory)}");
var memory2 = new List<string>();
//foreach(var item in memory)
//{
//    for (int i = 0; i < item.length; i++)
//        memory2.Add(item.value);
//}

var checksum = memory2
    .Select((i, p) => (i != "." ? long.Parse(i) : 0) * p)
    .Sum();

Console.WriteLine($"Checksum: {checksum}");