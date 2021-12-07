var instructions = File.ReadAllLines(Path.GetFullPath("input.txt")); // read instructions from file
var fileInput = instructions[0].Split(',').Select(x=>int.Parse(x));
Dictionary<int, long> nums = new Dictionary<int, long>();
long result = 0;
foreach (var input in fileInput)
    result += Recursion((6 - input) + 80);
Console.WriteLine($"{result}");

var example = new int[]{3, 4, 3, 1, 2};

result = 0;
foreach (var input in fileInput)
    result += Recursion((6 - input) + 256);
Console.WriteLine($"{result}");

long Recursion(int input)
{
    long output = 1;
    if (nums.ContainsKey(input))
        return nums[input];
    else if (input < 7)
        output = 1;
    else
        output = Recursion(input - 7) + Recursion(input - 9);
    nums[input] = output;
    return output;
}