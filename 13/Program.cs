var setup = File.ReadAllLines(Path.GetFullPath("input.txt"));
var rowIndex = 0;
var grid = new List<int[]>();
var maxX = 0;
var maxY = 0;
while (rowIndex < setup.Count() && !string.IsNullOrEmpty(setup[rowIndex]))
{
    grid.Add(setup[rowIndex].Split(',').Select(x => int.Parse(x)).ToArray());
    maxX = Math.Max(grid[grid.Count - 1][1], maxX);
    maxY = Math.Max(grid[grid.Count - 1][0], maxY);
    rowIndex++;
}
rowIndex++;
var instructions = new List<string>();
while (rowIndex < setup.Count())
{
    var row = setup[rowIndex];
    var rowLength = row.Split(' ').Count();
    instructions.Add(row.Split(' ')[rowLength - 1]);
    rowIndex++;
}

var instruction = instructions[0].Split('=');
var arrayComparer = new ArrayComparer();
if (instruction[0] == "x")
    grid = FoldX(int.Parse(instruction[1]), grid);
else if (instruction[0] == "y")
    grid = FoldY(int.Parse(instruction[1]), grid);

Console.WriteLine($"{grid.Count}");

for (int i = 1; i < instructions.Count; i++)
{
    instruction = instructions[i].Split('=');
    if (instruction[0] == "x")
        grid = FoldX(int.Parse(instruction[1]), grid);
    else if (instruction[0] == "y")
        grid = FoldY(int.Parse(instruction[1]), grid);
}

var MaxX = grid.Select(x=>x[0]).Max() + 1;
var MaxY = grid.Select(x=>x[1]).Max() + 1;

var outputGrid = new char[MaxY][];

for (int i = 0; i < MaxY; i++)
{
    outputGrid[i] = new char[MaxX];
    for (int j = 0; j < MaxX; j++)
    {
        outputGrid[i][j] = '-';
    }
}

for (int i = 0; i < grid.Count; i++)
{
    outputGrid[grid[i][1]][grid[i][0]] = '#';
}

for (int i = 0; i < outputGrid.Length; i++)
    Console.WriteLine($"{new string(outputGrid[i])}");

List<int[]> FoldX(int x, List<int[]> grid)
{
    grid = grid.OrderBy(p => p[0]).ToList();
    var index = grid.Count - 1;
    while (grid[index][0] > x)
    {
        var diff = grid[index][0] - x;
        grid[index][0] = x - diff;
        index--;
    }
    return grid.Where(p => p[0] != x).Distinct(arrayComparer).ToList();
}

List<int[]> FoldY(int y, List<int[]> grid)
{
    grid = grid.OrderBy(p => p[1]).ToList();
    var index = grid.Count - 1;
    while (grid[index][1] > y)
    {
        var diff = grid[index][1] - y;
        grid[index][1] = y - diff;
        index--;
    }
    return grid.Where(p => p[1] != y).Distinct(arrayComparer).ToList();
}

public class ArrayComparer : IEqualityComparer<int[]>
{
    public bool Equals(int[] a, int[] b)
    {
        if (a.Length != b.Length)
            return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
                return false;
        }
        return true;
    }

    public int GetHashCode(int[] a)
    {
        return a.Sum();
    }
}