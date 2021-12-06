var instructions = File.ReadAllLines(Path.GetFullPath("input.txt")); // read instructions from file
var nums = instructions.Select(x => x.Split(new string[] { ",", " -> " }, new StringSplitOptions()).Select(y => int.Parse(y))); // parse instructions into ints
var grid = new int[1000, 1000]; // all of our inputs are under 1000
var output = 0;
foreach (var r in nums)
{
    var row = r.ToArray();
    if (row[0] == row[2]) // (a, b) -> (a, c)
    {
        var a = row[1] < row[3] ? row[1] : row[3];
        var b = row[1] < row[3] ? row[3] : row[1];
        for (int i = a; i <= b; i++)
        {
            if (grid[row[0], i] == 1)
                output++;
            grid[row[0], i] += 1;
        }
    }
    else if (row[1] == row[3]) // (a, b) -> (c, b)
    {
        var a = row[0] < row[2] ? row[0] : row[2];
        var b = row[0] < row[2] ? row[2] : row[0];
        for (int i = a; i <= b; i++)
        {
            if (grid[i, row[1]] == 1)
                output++;
            grid[i, row[1]] += 1;
        }
    }
}
Console.WriteLine($"output: {output}");