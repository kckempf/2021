var input = File.ReadAllLines(Path.GetFullPath("input.txt"));

var pairs = new Dictionary<string, ulong>();    // Keep track of the pairs
var chars = new ulong[26];                      // Keep count of each character
var template = input[0];                        
for (int i = 0; i < template.Length - 1; i++)   // Initialize with the template
{
    chars[template[i] - 'A'] += 1;
    var pair = template.Substring(i, 2);
    if (pairs.ContainsKey(pair))
        pairs[pair] += 1;
    else
        pairs[pair] = 1;
}
chars[template[template.Length - 1] - 'A'] += 1;    // Add last letter
var mappings = new Dictionary<string, string[]>();  // The transforms create two pairs from one pair
for (int i = 2; i < input.Count(); i++)
{
    var row = input[i].Split(" -> ");
    var first = row[0][0] + row[1];
    var second = row[1] + row[0][1];
    if (!pairs.ContainsKey(first)) pairs[first] = 0;
    if (!pairs.ContainsKey(second)) pairs[second] = 0;
    mappings.Add(row[0], new string[] { first, second });
}

Console.WriteLine($"{CalculateResult(pairs, chars, mappings, 10)}");
Console.WriteLine($"{CalculateResult(pairs, chars, mappings, 40)}");

ulong CalculateResult(Dictionary<string, ulong> pairs, ulong[] chars, Dictionary<string, string[]> mappings, int steps)
{
    for (int i = 0; i < steps; i++)
    {
        var nextPairs = new Dictionary<string, ulong>();    // chars has all single characters, we just need pairs
        foreach (var pair in pairs)
        {
            var replace = mappings[pair.Key];               // Get the two pairs to replace the input pair
            if (nextPairs.ContainsKey(replace[0]))
                nextPairs[replace[0]] += pair.Value;
            else
                nextPairs[replace[0]] = pair.Value;
            if (nextPairs.ContainsKey(replace[1]))
                nextPairs[replace[1]] += pair.Value;
            else
                nextPairs[replace[1]] = pair.Value;

            var newChar = replace[0][1];                    // Only new character is the inserted one
            chars[newChar - 'A'] += pair.Value;
        }
        pairs = nextPairs;                                  // Pass the new pairs to the next step
    }

    ulong min = ulong.MaxValue;
    ulong max = ulong.MinValue;
    for (int i = 0; i < chars.Length; i++)                  // Get min and max
    {
        if (chars[i] != 0)
            min = Math.Min(chars[i], min);
        max = Math.Max(chars[i], max);
    }
    return max - min;                                       // Return difference
}