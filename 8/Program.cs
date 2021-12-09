var instructions = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x => x.Split('|').Select(y => y.Trim().Split(' ').ToArray()).ToArray());
var output = 0;
foreach (var value in instructions)
{
    var outputValues = value[1];
    for (int j = 0; j < outputValues.Length; j++)
    {
        switch (outputValues[j].Length)
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
output = 0;

foreach (var value in instructions)
{
    var dict = new Dictionary<string, char>();
    var outputValues = value[0].Distinct().Select(x=>string.Concat(x.OrderBy(y=>y))).ToList();
    var entriesList = value[1].Select(x=>string.Concat(x.OrderBy(y=>y))).ToList();
    var fives = new List<string>();
    var sixes = new List<string>();
    var two = string.Empty;
    var three = string.Empty;
    var four = string.Empty;
    foreach (var val in outputValues)
    {
        switch (val.Length)
        {
            case 2:
                dict[val] = '1';
                two = val;
                break;
            case 3:
                dict[val] = '7';
                three = val;
                break;
            case 4:
                dict[val] = '4';
                four = val;
                break;
            case 5:
                fives.Add(val);
                break;
            case 6:
                sixes.Add(val);
                break;
            case 7:
                dict[val] = '8';
                break;
            default:
                break;
        }
    }
    ProcessFives(dict, two, four, fives);
    ProcessSixes(dict, two, four, sixes);
    var entry = string.Empty;
    foreach (var en in entriesList)
    {
        entry += dict[en];
    }
    output += Convert.ToInt32(entry, 10);
}

Console.WriteLine($"{output}");

void ProcessFives(Dictionary<string, char> dict, string two, string four, List<string> fives)
{
    List<string> process = fives.Select(x => x).ToList();
    for (int i = 0; i < two.Length; i++)
    {
        process = process.Select(x => x.Replace(two[i].ToString(), string.Empty)).ToList();
    }
    int index = process.IndexOf(process.Where(x => x.Length == 3).FirstOrDefault());
    dict[fives[index]] = '3';
    fives.RemoveAt(index);
    process.RemoveAt(index);
    for (int i = 0; i < four.Length; i++)
    {
        process = process.Select(x => x.Replace(four[i].ToString(), string.Empty)).ToList();
    }
    for (int i = 0; i < process.Count; i++)
    {
        if (process[i].Length == 2)
            dict[fives[i]] = '5';
        else if (process[i].Length == 3)
            dict[fives[i]] = '2';
    }
}

void ProcessSixes(Dictionary<string, char> dict, string two, string four, List<string> sixes)
{
    List<string> process = sixes.Select(x => x).ToList();
    for (int i = 0; i < two.Length; i++)
    {
        process = process.Select(x => x.Replace(two[i].ToString(), string.Empty)).ToList();
    }
    int index = process.IndexOf(process.Where(x => x.Length == 5).FirstOrDefault());
    dict[sixes[index]] = '6';
    sixes.RemoveAt(index);
    process.RemoveAt(index);
    for (int i = 0; i < four.Length; i++)
    {
        process = process.Select(x => x.Replace(four[i].ToString(), string.Empty)).ToList();
    }
    for (int i = 0; i < process.Count; i++)
    {
        if (process[i].Length == 2)
            dict[sixes[i]] = '9';
        else if (process[i].Length == 3)
            dict[sixes[i]] = '0';
    }
}