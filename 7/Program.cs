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