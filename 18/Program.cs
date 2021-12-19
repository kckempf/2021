//var input = File.ReadAllLines(Path.GetFullPath("input.txt"));
var input = new string[]
{
    "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
    "[[[5,[2,8]],4],[5,[[9,9],0]]]",
    "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
    "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
    "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
    "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
    "[[[[5,4],[7,7]],8],[[8,3],8]]",
    "[[9,3],[[9,9],[6,[4,9]]]]",
    "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
    "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"
};

var blah = input[0];
for (int i = 1; i < input.Count(); i++)
{
    blah = Add(blah, input[i]);
    Console.WriteLine($"{i}: {blah}");
}
string Add(string a, string b)
{
    string output = $"[{a},{b}]";
    Console.WriteLine(output);
    return Reduce(output);
}

string Reduce(string input)
{
    var inputArray = input.ToCharArray().ToList();
    var firstNumeric = -1;
    var isPrevNumeric = false;
    var previousNumber = -1;
    var openCount = 0;

    for (int i = 0; i < input.Length; i++)
    {
        if (input[i] == '[')
        {
            openCount++;
            if (openCount > 4)
                return Reduce(Explode(input, previousNumber, i));
        }
        else if (input[i] == ']')
        {
            openCount--;
        }
        else if (char.IsNumber(input[i]))
        {
            if (i > 0 && previousNumber == i - 1)
            {
                return Reduce(Split(input, i - 1));
            }
            else
                previousNumber = i;
        }
    }

    return input;
}

string Split(string input, int first)
{
    var splitString = input.Substring(first, 2);
    var splitInput = input.Split(splitString);
    var splitInt = int.Parse(splitString);
    var insertString = $"[{splitInt / 2},{splitInt / 2 + splitInt % 2}]";
    return splitInput[0] + insertString + splitInput[1];
}

string Explode(string input, int previousNum, int fifthBracket)
{
    int closeBracket = fifthBracket;
    while (input[closeBracket] != ']')
        closeBracket++;
    var pair = input.Substring(fifthBracket + 1, closeBracket - fifthBracket - 1).Split(',');
    int nextNum = closeBracket;
    
    int N = input.Length;
    while (nextNum < input.Length && !char.IsNumber(input[nextNum]))
        nextNum++;
    if (nextNum < N)
    {
        var nextNumInt = int.Parse(pair[1]) + (input[nextNum] - '0');
        input = input.Substring(0, nextNum) + nextNumInt.ToString() + input.Substring(nextNum + 1, N - nextNum - 1);
    }
    if (previousNum > 0)
    {
        var previousNumInt = int.Parse(pair[0]) + (input[previousNum] - '0');
        if (previousNumInt > 9)
        {
            fifthBracket++;
            closeBracket++;
        }
        input = input.Substring(0, previousNum) + previousNumInt.ToString() + input.Substring(previousNum + 1, input.Length - previousNum - 1);
    }
    return input.Substring(0, fifthBracket) + "0" + input.Substring(closeBracket + 1, input.Length - closeBracket - 1);
}

// Explode: 3 steps:
// 1. Add x to previous number
// 2. Add y to next number
// 3. replace [x,y] with 0

// Split: 1 step
// 1. Replace NN with [NN / 2, NN / 2 + NN % 2]

// Reduce: 
// 1. increment until we hit the first condition
// 2. if 5th open [ before next ], trigger Explode and call Reduce
// 3. if consecutive numeric, trigger Split and call Reduce

