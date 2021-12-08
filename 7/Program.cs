var instructions = File.ReadAllLines(Path.GetFullPath("input.txt"))[0].Split(',').Select(x=>int.Parse(x)).ToArray();
Array.Sort(instructions);
int result = instructions.Sum(x => Math.Abs(instructions[0] - x));
for (int i = 1; i < instructions.Length; i++)
{
    var sum = instructions.Sum(x => Math.Abs(instructions[i] - x));
    if (sum <= result)
        result = sum;
    else
        break;
}
Console.WriteLine($"{result}");

var first = instructions[0];
var last = instructions[instructions.Length - 1];
var midSum = 0;
while (first < last)
{
    var mid = first + (last - first) / 2;
    midSum = instructions.Sum(x=>InnerSum(x, mid));
    var beforeSum = instructions.Sum(x=>InnerSum(x, mid - 1));
    var afterSum = instructions.Sum(x=>InnerSum(x, mid + 1));
    if (midSum < beforeSum && midSum < afterSum)
    {
        break;
    }
    else if (midSum > beforeSum)
    {
        last = mid - 1;
    }
    else if (midSum > afterSum)
    {
        first = mid;
    }
}

Console.WriteLine($"{midSum}");
int InnerSum(int a, int b)
{
    var n = Math.Abs(a - b);
    return (n * (n + 1)) / 2;
}