var depths = File.ReadAllLines(Path.GetFullPath("input.txt"));
int.TryParse(depths[0], out int depth);
var output = 0;
for (int i = 1; i < depths.Length; i++)
{
    int.TryParse(depths[i], out int currentDepth);
    if (currentDepth > depth)
        output++;
    depth = currentDepth;
}
Console.WriteLine($"Answer 1: {output}");

output = 0;
for (int i = 3; i < depths.Length; i++)
{
    int.TryParse(depths[i], out int currentDepth);
    int.TryParse(depths[i - 3], out int pastDepth);
    if (currentDepth > pastDepth)
        output++;
}
Console.WriteLine($"Answer 2: {output}");