var octopi = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x => x.ToCharArray().Select(y => y - '0').ToArray()).ToArray();

var dirs = new int[][] {
    new int[] {-1, 0},
    new int[] {0, -1},
    new int[] {1, 0},
    new int[] {0, 1},
    new int[] { -1, -1},
    new int[] {-1, 1},
    new int[] {1, -1},
    new int[] {1, 1}
};
var output = 0;
var flashSync = -1;
int step = 0;
while (flashSync != 0)
{
    ProcessFlashes();
    if (step == 100)
        Console.WriteLine($"{output}");
    flashSync = octopi.Sum(x=>x.Sum(y=>y));
    step++;
}
Console.WriteLine($"{step}");

void ProcessFlashes()
{
    Queue<int[]> flashes = new Queue<int[]>();
    for (int i = 0; i < octopi.Length; i++)
    {
        for (int j = 0; j < octopi[i].Length; j++)
        {
            if (octopi[i][j] < 9)
            {
                octopi[i][j] += 1;
            }
            else
            {
                octopi[i][j] = 0;
                flashes.Enqueue(new int[] { i, j });
                output += 1;
            }
        }
    }
    while (flashes.Count > 0)
    {
        var flash = flashes.Dequeue();
        for (int i = 0; i < dirs.Length; i++)
        {
            int x = flash[0] + dirs[i][0];
            int y = flash[1] + dirs[i][1];
            if (x >= 0 &&
                x < octopi.Length &&
                y >= 0 &&
                y < octopi[x].Length)
            {
                if (octopi[x][y] == 0)
                {
                }
                else if (octopi[x][y] < 9)
                {
                    octopi[x][y] += 1;
                }
                else
                {
                    octopi[x][y] = 0;
                    flashes.Enqueue(new int[] { x, y });
                    output += 1;
                }
            }
        }
    }
}