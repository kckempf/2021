// See https://aka.ms/new-console-template for more information
var instructions = File.ReadAllLines(Path.GetFullPath("input.txt"));
var depth = 0;
var x = 0;
foreach (var step in instructions)
{
    var split = step.Split(' ');
    var direction = split[0];
    int.TryParse(split[1], out int amp);
    switch (direction)
    {
        case "forward":
            x += amp;
            break;
        case "down":
            depth += amp;
            break;
        case "up":
            depth -= amp;
            break;
        default:
            break;
    }
}
Console.WriteLine($"{x * depth}");
depth = 0;
x = 0;
var aim = 0;
foreach (var step in instructions)
{
    var split = step.Split(' ');
    var direction = split[0];
    int.TryParse(split[1], out int amp);
    switch (direction)
    {
        case "forward":
            x += amp;
            depth += amp * aim;
            break;
        case "down":
            aim += amp;
            break;
        case "up":
            aim -= amp;
            break;
        default:
            break;
    }
}
Console.WriteLine($"{x * depth}");