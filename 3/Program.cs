// See https://aka.ms/new-console-template for more information
var instructions = File.ReadAllLines(Path.GetFullPath("input.txt"));
var counts = new int[instructions[0].Length];
for (int i = 0; i < instructions.Length; i++)
{
    for (int j = 0; j < instructions[i].Length; j++)
    {
        if (instructions[i][j] == '1')
            counts[j] += 1;
    }
}
var gamma = instructions[0].ToCharArray();
var epsilon = instructions[0].ToCharArray();
var M = instructions.Length / 2;
for (int i = 0; i < counts.Length; i++)
{
    if (counts[i] >= M)
    {
        gamma[i] = '1';
        epsilon[i] = '0';
    }
    else
    {
        gamma[i] = '0';
        epsilon[i] = '1';
    }
}
var gammaInt = Convert.ToInt32(new string(gamma), 2);
var epsilonInt = Convert.ToInt32(new string(epsilon), 2);
Console.WriteLine($"{gammaInt * epsilonInt}");

List<string> oxyList = new List<string>(instructions);
List<string> co2List = new List<string>(instructions);

for (int i = 0; i < instructions[0].Length && oxyList.Count > 1; i++)
{
    var total = 0;
    for (int j = 0; j < oxyList.Count; j++)
    {
        if (oxyList[j][i] == '1')
            total++;
    }
    var mostCommon = total >= oxyList.Count / 2 ? '1' : '0';
    oxyList = oxyList.Where(x=>x[i] == mostCommon).ToList();
}

var oxyRating = Convert.ToInt32(oxyList[0], 2);

for (int i = 0; i < instructions[0].Length && co2List.Count > 1; i++)
{
    var total = 0;
    for (int j = 0; j < co2List.Count; j++)
    {
        if (co2List[j][i] == '1')
            total++;
    }
    var leastCommon = total >= co2List.Count / 2 ? '0' : '1';
    co2List = co2List.Where(x=>x[i] == leastCommon).ToList();
}

var co2Rating = Convert.ToInt32(co2List[0], 2);

Console.WriteLine($"{oxyRating * co2Rating}");