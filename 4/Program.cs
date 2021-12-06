// See https://aka.ms/new-console-template for more information
var instructions = File.ReadAllLines(Path.GetFullPath("input.txt"));
var hash = new Dictionary<int, List<int>>(); // map between current number and List of indices with it
var sums = new List<int>(); // list of sums of the solutions
var curr = new List<int>(); // list of how many numbers selected
var nums = instructions[0].Split(','); // numbers as they are called, for later
List<int[]> board = new List<int[]>(); // each of our boards is an array of ints
Dictionary<int, int> boards = new Dictionary<int, int>(); // indexes of rows to boards
var boardTotals = new List<int>(); // totals of number left on boards
int boardnum = 0;
for (int i = 2; i < instructions.Length; i++)
{
    if (string.IsNullOrEmpty(instructions[i]))
    {
        var diag1 = new int[board.Count];
        var diag2 = new int[board.Count];
        var boardTotal = board.Select(x => x.Sum()).Sum();
        boardTotals.Add(boardTotal);
        for (int j = 0; j < board.Count; j++)
        {
            var row = board[j];
            AddRow(row, hash, sums, curr, boardTotal, boards, boardnum);
            row = board.Select(x => x[j]).ToArray();
            diag1[j] = row[j];
            diag2[j] = row[row.Length - j - 1];
            AddRow(row, hash, sums, curr, boardTotal, boards, boardnum);
        }
        AddRow(diag1, hash, sums, curr, boardTotal, boards, boardnum);
        AddRow(diag2, hash, sums, curr, boardTotal, boards, boardnum);
        board = new List<int[]>();
        boardnum++;
    }
    else
    {
        var row = instructions[i].Split(' ').Select(x => Int32.TryParse(x, out int intX) ? intX : -1).Where(x => x != -1).ToArray();
        board.Add(row);
    }
}

for (int i = 0; i < nums.Length; i++)
{
    int.TryParse(nums[i], out int currentNum);
    var s = hash[currentNum];
    foreach (var index in s)
    {
        curr[index] -= 1;
        boardTotals[boards[index]] -= currentNum;
        if (curr[index] == 0)
        {
            Console.WriteLine($"{boardTotals[boards[index]] * currentNum}");
            Console.WriteLine($"winner: {currentNum} board: {boards[index]}");
            return;
        }
    }
}

static void AddRow(int[] row, Dictionary<int, List<int>> hash, List<int> sums, List<int> curr, int boardTotal, Dictionary<int, int> boards, int boardnum)
{
    var sum = row.Sum();
    sums.Add(boardTotal - sum);
    curr.Add(row.Length);
    for (int j = 0; j < row.Length; j++)
    {
        if (!hash.ContainsKey(row[j]))
        {
            hash[row[j]] = new List<int>();
        }
        hash[row[j]].Add(sums.Count - 1);
        boards[sums.Count - 1] = boardnum;
    }
}

Console.WriteLine("Hello, World!");


// 1  2  3  4  5
// 6  7  8  9  10 
// 11 12 13 14 15
// 16 17 18 19 20
// 21 22 23 24 25

// 1  2  3  4  5    // col[i] = line.split)
// 6  7  8  9  10 
// 11 12 13 14 15
// 16 17 18 19 20
// 21 22 23 24 25
// 1  6  11 16 21   // col[i + 5].Add(line(j))
// 2  7  12 17 22
// 3  8  13 18 23
// 4  9  14 19 24
// 5  10 15 20 25
// 1  7  13 19 25  // if i == j col[i + 10].Add(line(j))
// 5  9  13 17 21