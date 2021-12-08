var instructions = File.ReadAllLines(Path.GetFullPath("input.txt"));    // read instructions from file
var fileInput = instructions[0].Split(',').Select(x=>int.Parse(x));     // convert instructions to ints
Dictionary<int, long> nums = new Dictionary<int, long>();               // memoization dictionary
long result = 0;
foreach (var input in fileInput)
    result += Recursion((6 - input) + 80);                              // call the recursive function for each
Console.WriteLine($"{result}");                                         // each one takes 6 - n steps further than 80

var example = new int[]{3, 4, 3, 1, 2};

result = 0;
foreach (var input in fileInput)
    result += Recursion((6 - input) + 256);                             // part two
Console.WriteLine($"{result}");

long Recursion(int input)
{
    long output = 1;                                                    // result is more than 2^32 - 1
    if (nums.ContainsKey(input))                                        // use memoization to only calculate once
        return nums[input];                     
    else if (input < 7)                                                 // start with one lanternfish
        output = 1;
    else
        output = Recursion(input - 7) + Recursion(input - 9);           // add another every 7 steps; it's 2 steps behind
    nums[input] = output;
    return output;
}