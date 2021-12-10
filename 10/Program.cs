var instructions = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x => x.ToCharArray()).ToArray();
var illegalScores = new Dictionary<char, int>()
{
    { ')', 3},
    { ']', 57},
    { '}', 1197},
    { '>', 25137}
};
var charScores = new Dictionary<char, int>()
{
    { ')', 1},
    { ']', 2},
    { '}', 3},
    { '>', 4}
};
var legalEnds = new Dictionary<char, char>()
{
    { '(', ')'},
    { '[', ']'},
    { '{', '}'},
    { '<', '>'}
};
var output = 0;
var completionScores = new List<long>();
foreach (var instruction in instructions)
{
    var stack = new Stack<char>();                      
    long completionScore = 0;
    var legal = true;
    for (int i = 0; i < instruction.Length; i++)
    {
        var currentChar = instruction[i];
        if (legalEnds.ContainsKey(currentChar))                 // Stack the legal ends when we see a start
        {
            stack.Push(legalEnds[currentChar]);
        }
        else
        {
            var expectedChar = stack.Pop();
            if (currentChar != expectedChar)                    // If end is unexpected, corrupted
            {
                output += illegalScores[currentChar];
                legal = false;
                break;
            }
        }
    }
    while (stack.Count > 0 && legal)                            // stack has all the missing ends
    {
        var expectedChar = stack.Pop();
        completionScore *= 5;
        completionScore += charScores[expectedChar];
    }
    if (legal)
        completionScores.Add(completionScore);
}
completionScores.Sort();
var output2 = completionScores[(completionScores.Count / 2)];  
Console.WriteLine($"{output}");
Console.WriteLine($"{output2}");