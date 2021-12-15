var input = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x => x.ToCharArray().Select(x => x - '0').ToArray()).ToArray();
var dirs = new int[][]{                         // Set up the directions
    new int[]{-1, 0},
    new int[]{0, -1},
    new int[]{1, 0},
    new int[]{0, 1}
};
var pq = new PriorityQueue<int[], int>();       // Initialize Priority Queue
pq.Enqueue(new int[] { 0, 0 }, 0);
var cell = new int[] { 0, 0 };
var output = 0;
var sum = 0;
while (pq.Count > 0 && (cell[0] < input.Length - 1 || cell[1] < input[0].Length - 1))  // Dykstra's Shortest Path
{
    pq.TryDequeue(out cell, out sum);
    Console.WriteLine($"{cell[0]},{cell[1]}, sum: {sum}");
    foreach (var dir in dirs)
    {
        var nextCell = new int[] { cell[0] + dir[0], cell[1] + dir[1] };
        if (
            nextCell[0] > -1 &&
            nextCell[0] < input.Length &&
            nextCell[1] > -1 &&
            nextCell[1] < input[0].Length &&
            input[nextCell[0]][nextCell[1]] != -1)
        {
            var density = sum + input[nextCell[0]][nextCell[1]];
            input[nextCell[0]][nextCell[1]] = -1;
            pq.Enqueue(nextCell, density);
        }
    }
}
Console.WriteLine($"sum: {sum}, pq.Count: {pq.Count}");