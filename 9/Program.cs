var instructions = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x => x.ToCharArray()).ToArray();
var sample = new string[]
{
    "2199943210",
    "3987894921",
    "9856789892",
    "8767896789",
    "9899965678"
}.Select(x => x.ToCharArray()).ToArray();
var dirs = new int[][] {
    new int[] { -1, 0},
    new int[] { 0, -1},
    new int[] { 0, 1},
    new int[] { 1, 0}
};
var output = 0;
for (int i = 0; i < instructions.Length; i++)
{
    for (int j = 0; j < instructions[i].Length; j++)
    {
        var sum = 0;
        for (int k = 0; k < dirs.Length; k++)
        {
            var x = j + dirs[k][1];
            var y = i + dirs[k][0];

            if (
                x < 0 ||
                y < 0 ||
                x >= instructions[i].Length ||
                y >= instructions.Length ||
                (int)instructions[y][x] - '0' > (int)instructions[i][j] - '0')
            {
                sum++;
            }
        }
        if (sum == 4)
        {
            output += instructions[i][j] - '0' + 1;
        }
    }
}
Console.WriteLine($"{output}");
output = 0;
var outputCollection = new List<int>();
for (int i = 0; i < instructions.Length; i++)
{
    for (int j = 0; j < instructions[i].Length; j++)
    {
        if (instructions[i][j] != '9')
        {
            outputCollection.Add(Recursion(0, i, j));   // Recursion will mark space with 9 when it traverses
        }
    }
}

output = outputCollection
    .OrderByDescending(x => x)
    .Take(3)
    .Aggregate(1, (x, y) => x * y);                     // Take product of top 3

Console.WriteLine($"{output}");

int Recursion(int sum, int i, int j)
{
    if (i < 0 ||
        i >= instructions.Length ||
        j < 0 ||
        j >= instructions[i].Length ||
        instructions[i][j] == '9')
        return sum;                                     // Recursion stops at edge or a 9
    sum++;
    instructions[i][j] = '9';                           // Mark space as 9 so we don't repeat
    foreach (var dir in dirs)
    {
        sum += Recursion(0, i + dir[0], j + dir[1]);    // Add the neighbors to the list
    }
    return sum;
}