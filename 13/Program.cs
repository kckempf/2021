var setup = File.ReadAllLines(Path.GetFullPath("input.txt"));
// var setup = new string[]{
//     "6,10",
//     "0,14",
//     "9,10",
//     "0,3",
//     "10,4",
//     "4,11",
//     "6,0",
//     "6,12",
//     "4,1",
//     "0,13",
//     "10,12",
//     "3,4",
//     "3,0",
//     "8,4",
//     "1,10",
//     "2,14",
//     "8,10",
//     "9,0"
// };
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
if (instruction[0] == "x")
    grid = FoldX(int.Parse(instruction[1]), grid);
else if (instruction[0] == "y")
    grid = FoldY(int.Parse(instruction[1]), grid);
grid = grid.Distinct(new ArrayComparer()).ToList();
Console.WriteLine($"{grid.Count}");

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
    return grid.Where(p => p[0] != x).ToList();
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
    return grid.Where(p => p[1] != y).ToList();
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

// ...#..#..#.  0 = 7 - 7
// ....#......  1 = 7 - 6
// ...........  2 = 7 - 5
// #..........  3 = 7 - 4
// ...#....#.#  4 = 7 - 3
// ...........
// ........... 
// ...........  7
// ...........
// ...........
// .#....#.##.  10 = 7 + 3   1,10 -> 1,4 | 6,10 -> 6,4
// ....#......  11 = 7 + 4
// ......#...#  12 = 7 + 5
// #..........  13 = 7 + 6
// #.#........  14 = 7 + 7   2,14 -> 2,0