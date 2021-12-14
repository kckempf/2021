var input = File.ReadAllLines(Path.GetFullPath("input.txt"));
var sample = new string[]{
    "NNCB",
    string.Empty,
    "CH -> B",
    "HH -> N",
    "CB -> H",
    "NH -> C",
    "HB -> C",
    "HC -> B",
    "HN -> C",
    "NN -> C",
    "BH -> H",
    "NC -> B",
    "NB -> B",
    "BN -> B",
    "BB -> N",
    "BC -> B",
    "CC -> N",
    "CN -> C"
};

var template = input[0];
var mappings = new Dictionary<string, char>();

for (int i = 2; i < input.Length; i++)
{
    var row = input[i].Split(" -> ");
    mappings.Add(row[0], row[1][0]);
}

for (int j = 0; j < 10; j++)
{
    var templateList = template.ToCharArray().ToList();
    for (int i = template.Length - 1; i > 0; i--)
    {
        var segment = template.Substring(i - 1, 2);
        if (mappings.ContainsKey(segment))
        {
            templateList.Insert(i, mappings[segment]);
        }
    }
    template = new string(templateList.ToArray());
}

var m = template.ToCharArray().ToList().GroupBy(x => x).OrderByDescending(y=>y.Count()).Select(p=>p.Count()).FirstOrDefault();
var n = template.ToCharArray().ToList().GroupBy(x => x).OrderBy(y=>y.Count()).Select(p=>p.Count()).FirstOrDefault();
Console.WriteLine($"{m - n}");