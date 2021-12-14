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

var pairs = new Dictionary<string, ulong>();
var chars = new ulong[26];
var template = input[0];
for (int i = 0; i < template.Length - 1; i++)
{
    chars[template[i] - 'A'] += 1;
    var pair = template.Substring(i, 2);
    if (pairs.ContainsKey(pair))
        pairs[pair] += 1;
    else
        pairs[pair] = 1;
}
chars[template[template.Length - 1] - 'A'] += 1;
var mappings = new Dictionary<string, string[]>();
for (int i = 2; i < input.Count(); i++)
{
    var row = input[i].Split(" -> ");
    var first = row[0][0] + row[1];
    var second = row[1] + row[0][1];
    mappings.Add(row[0], new string[] { first, second });
}

for (int i = 0; i < 10; i++)
{
    foreach (var pair in pairs)
    {
        var replace = mappings[pair.Key];
        pairs[pair.Key] -= 1;
        pairs[replace[0]] += 1;
        pairs[replace[1]] += 1;
        var newChar = replace[0][1];
        chars[newChar - 'A'] += 1;
    }
}

var min = chars.Min();
var max = chars.Max();

Console.WriteLine($"{max - min}");


// "CH -> B",   CB, BH   b+
// "HH -> N",   HN, NH
// "CB -> H",   CH, BH
// "NH -> C",   NC, CH
// "HB -> C",   HC, CB
// "HC -> B",   HB, BC
// "HN -> C",   HC, CN
// "NN -> C",   NC, CN
// "BH -> H",   BH, HH
// "NC -> B",   NB, BC
// "NB -> B",   NB, BB
// "BN -> B",   BB, BN
// "BB -> N",   BN, NB
// "BC -> B",   BB, BC
// "CC -> N",   CN, NC
// "CN -> C"    CC, CN