//var edges = File.ReadAllLines(Path.GetFullPath("input.txt")).Select(x => x.Split('-').ToArray()).ToArray();
var edges = new string[]
{
    "fs-end",
    "he-DX",
    "fs-he",
    "start-DX",
    "pj-DX",
    "end-zg",
    "zg-sl",
    "zg-pj",
    "pj-he",
    "RW-he",
    "fs-DX",
    "pj-RW",
    "zg-RW",
    "start-pj",
    "he-WI",
    "zg-he",
    "pj-fs",
    "start-RW"
}.Select(x => x.Split('-').ToArray()).ToArray();
var nodes = new Dictionary<string, Node>();

foreach (var edge in edges)
{
    foreach (var node in edge)
    {
        if (!nodes.ContainsKey(node))
        {
            nodes.Add(node, new Node(node, !Char.IsUpper(node, 0)));
        }
    }
    if (edge[1] != "start" && edge[0] != "end")
        nodes[edge[0]].Adjacency.Add(nodes[edge[1]]);
    if (edge[0] != "start" && edge[1] != "end")
        nodes[edge[1]].Adjacency.Add(nodes[edge[0]]);
}
var output = 0;
Recursion(nodes["start"]);
Console.WriteLine($"{output}");

var paths = new Dictionary<int, List<string>>();
output = Recursion2(nodes["start"], paths, 0);
Console.WriteLine($"{paths.Count}");

void Recursion(Node root)
{
    root.IsVisited = true;
    if (root.Name == "end")
    {
        output += 1;
        return;
    }
    foreach (var node in root.Adjacency)
    {
        if (!node.IsSmall || !node.IsVisited)
        {
            Recursion(node);
            node.IsVisited = false;
        }
    }
}

int Recursion2(Node root, Dictionary<int, List<string>> list, int attempt)
{
    var curr = list.ContainsKey(attempt) ? list[attempt] : new List<string>();
    curr.Add(root.Name);

    if (root.Name == "end")
    {
        list[attempt] = curr;
        return attempt + 1;
    }

    foreach (var node in root.Adjacency)
    {
        if (char.IsLower(node.Name[0]) && curr.Contains(node.Name))
            continue;
        list[attempt] = curr;
        attempt = Recursion2(node, list, attempt);
    }
    list.Remove(attempt);
    return attempt;
}

public class Node
{
    public bool IsSmall { get; set; }
    public bool IsVisited { get; set; }
    public List<Node> Adjacency { get; set; }
    public string Name { get; set; }

    public Node(string name, bool isSmall)
    {
        IsSmall = isSmall;
        Name = name;
        Adjacency = new List<Node>();
    }
}

    // "fs-end",
    // "he-DX",
    // "fs-he",
    // "start-DX",
    // "pj-DX",
    // "end-zg",
    // "zg-sl",
    // "zg-pj",
    // "pj-he",
    // "RW-he",
    // "fs-DX",
    // "pj-RW",
    // "zg-RW",
    // "start-pj",
    // "he-WI",
    // "zg-he",
    // "pj-fs",
    // "start-RW"