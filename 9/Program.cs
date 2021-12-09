var instructions = File.ReadAllLines(Path.GetFullPath("input.txt")).ToArray();
var sample = new string[]
{
    "2199943210",
    "3987894921",
    "9856789892",
    "8767896789",
    "9899965678"
};
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
