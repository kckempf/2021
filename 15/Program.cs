var input = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
var width = input.Length;
var output = ShortestPath(input, 5, width);

Console.WriteLine($"{output}");

int ShortestPath(int[][] input, int multiplier, int width)
{
    var dirs = new int[][]{                         // Set up the directions
        new int[]{-1, 0},
        new int[]{0, -1},
        new int[]{1, 0},
        new int[]{0, 1}
    };
    var visited = new HashSet<string>();            // Record whether we have been here before
    var pq = new PriorityQueue<int[], int>();       // Initialize Priority Queue
    pq.Enqueue(new int[] { 0, 0 }, 0);
    var cell = new int[] { 0, 0 };
    var sum = 0;
    while (
        pq.Count > 0 &&
        (cell[0] < input.Length * multiplier - 1 ||
        cell[1] < input[0].Length * multiplier - 1))    // Dykstra's Shortest Path
    {
        pq.TryDequeue(out cell, out sum);               // Prioriy Queue keeps items in order by value, by ascending numeric by default
        foreach (var dir in dirs)
        {
            var nextCell = new int[] { cell[0] + dir[0], cell[1] + dir[1] };
            var i = nextCell[0] % width;
            var j = nextCell[1] % width;
            if (
                nextCell[0] > -1 &&
                nextCell[0] < input.Length * multiplier &&
                nextCell[1] > -1 &&
                nextCell[1] < input[0].Length * multiplier &&
                !visited.Contains($"{nextCell[0]} - {nextCell[1]}"))    // Don't go where we have been
            {
                var addI = nextCell[0] / width;
                var addJ = nextCell[1] / width;
                var newSum = input[i][j];
                newSum = newSum + addI + addJ;
                newSum = newSum % 9;
                newSum = newSum == 0 ? 9 : newSum;
                var density = sum + newSum;
                visited.Add($"{nextCell[0]} - {nextCell[1]}");  // Record when we have been here
                pq.Enqueue(nextCell, density);                  // density = sum of density so far
            }
        }
    }
    return sum;
}