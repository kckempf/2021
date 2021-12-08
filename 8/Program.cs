var instructions = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x=>x.Split('|').Select(y=>y.Trim().Split(' ').ToArray()).ToArray());
var sample = @"be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe
edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc
fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |cg cg fdcagb cbg
fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |efabcd cedba gadfec cb
aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |gecf egdcabf bgf bfgea
fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |gebdcfa ecba ca fadegcb
dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |cefg dcbef fcge gbcadfe
bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |ed bcgafe cdgba cbgef
egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |gbdfcae bgc cg cgb
gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |fgae cfgab fg bagce".Split(new string[]{Environment.NewLine}, StringSplitOptions.None).Select(x=>x.Split('|').Select(y=>y.Trim().Split(' ').ToArray()).ToArray());
var output = 0;
foreach (var value in instructions)
{
    var outputValues = value[1];
    for (int j = 0; j < outputValues.Length; j++)
    {
        switch(outputValues[j].Length)
        {
            case 2:
            case 4:
            case 3:
            case 7:
                output++;
                break;
            default:
                break;
        }
    }
}

Console.WriteLine($"{output}");

// Visualization
//  AAA
// B   C
// B   C
//  DDD
// E   F
// E   F
//  GGG

// 0   ABC EFG  6
// 1     C  F   2
// 2   A CDE G  5
// 3   A CD FG  5
// 4    BCD F   4
// 5   AB D FG  5
// 6   AB DEFG  6
// 7   A C  F   3
// 8   ABCDEFG  7
// 9   ABCD FG  6