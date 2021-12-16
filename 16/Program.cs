using System.Text;

var packets = File.ReadAllLines(Path.GetFullPath("input.txt"));
//var packets = new string[] { "C0015000016115A2E0802F182340" };
var hexToBin = new Dictionary<char, string>()   // this will be less confusing than implicit conversion
{
    {'0',"0000"},
    {'1',"0001"},
    {'2',"0010"},
    {'3',"0011"},
    {'4',"0100"},
    {'5',"0101"},
    {'6',"0110"},
    {'7',"0111"},
    {'8',"1000"},
    {'9',"1001"},
    {'A',"1010"},
    {'B',"1011"},
    {'C',"1100"},
    {'D',"1101"},
    {'E',"1110"},
    {'F',"1111"}
};

int output = 0;
foreach (var packet in packets)
{
    output += CalculateVersionNumber(ConvertToBinary(packet));
}

Console.WriteLine($"{output}");

string ConvertToBinary(string input)
{
    var sb = new StringBuilder();
    for (int i = 0; i < input.Length; i++)
    {
        sb.Append(hexToBin[input[i]]);
    }
    return sb.ToString();
}

int CalculateVersionNumber(string input)
{
    int output = 0;

    if (input.Length < 5)
        return 0;

    // first 3 bits are the packet version
    var versionString = input.Substring(0, 3);
    var version = Convert.ToInt32(versionString, 2);
    input = input.Substring(3, input.Length - 3);
    output += version;

    if (input.Length < 5)
        return 0;

    // next 3 bits are the type ID
    var typeIdString = input.Substring(0, 3);
    var type = Convert.ToInt32(typeIdString, 2);
    input = input.Substring(3, input.Length - 3);


    if (input.Length < 5)
        return 0;

    // type == 4 means this is a literal
    if (type == 4)
    {
        var next = input.Substring(0, 5);
        input = input.Substring(5, input.Length - 5);
        while (next[0] != '0')
        {
            next = input.Substring(0, 5);
            input = input.Substring(5, input.Length - 5);
        }
        // pass along to the next string
        return output + CalculateVersionNumber(input);
    }

    // length type ID
    var lengthTypeIdString = input.Substring(0, 1);
    var lengthTypeId = Convert.ToInt32(lengthTypeIdString, 2);
    input = input.Substring(1, input.Length - 1);

    if (lengthTypeId == 0)
    {
        var totalLengthString = input.Substring(0, 15);
        var totalLength = Convert.ToInt32(totalLengthString, 2);
        input = input.Substring(15, input.Length - 15);
        var segment = input.Substring(0, totalLength);
        input = input.Substring(totalLength, input.Length - totalLength);
        return output + CalculateVersionNumber(segment) + CalculateVersionNumber(input);
    }
    else
    {
        var numberOfSubPacketsString = input.Substring(0, 11);
        var numberOfSubPackets = Convert.ToInt32(numberOfSubPacketsString, 2);
        input = input.Substring(11, input.Length - 11);
        return output + CalculateVersionNumber(input);
    }

    return output + CalculateVersionNumber(input);
}

// 011 000 1 00000000010 
// v:3 t:0 S           2 

// 000 000 0 000000000010110 
// v:0 t:0 L              22 

// 000 100 0 1010  
// v:0 t:4 

// 101 100 0 1011
// v:5 t:4

// 001 000 1 00000000010 
// v:1 t:0 S           2

// 000 100 0 1100 
// v:0 t:4

// 011 100 0 1101 00
// v:3 t:4