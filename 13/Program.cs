var setup = File.ReadAllLines(Path.GetFullPath("input.txt"));
var rowIndex = 0;
var grid = new List<int[]>();
var maxX = 0;
var maxY = 0;
while (rowIndex < setup.Count)
{
    grid.Add(setup[rowIndex].Split(',').Select(x=>int.Parse(x)));
    maxX = Math.Max(grid[grid.Count - 1][1], maxX);
    maxY = Math.Max(grid[grid.Count - 1][0], maxY);
    rowIndex++;
}

void FoldX(int x)
{
    
}

void FoldY(int y)
{
    grid.OrderBy(x=>x[1]);
    for (int i = grid.Count - 1; grid[i][1] > x; i--)
    {
        
    }
}

// ...#..#..#.
// ....#......
// ...........
// #..........
// ...#....#.#  C
// ...........  B
// ...........  A
// ...........
// ...........  < ---
// ...........
// .#....#.##.  A
// ....#......  B
// ......#...#  C
// #..........
// #.#........