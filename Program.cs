var fullInput =
@"43
3
4
10
21
44
4
6
47
41
34
17
17
44
36
31
46
9
27
38";

var smallInput =
@"20
15
10
5
5";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var containers = input.Split(Environment.NewLine).Select(int.Parse).Select(x => new Node { Value = x }).ToList();
var volume = containers.Count == 5 ? 25 : 150;




var combinations = new HashSet<string>();
int i = 0;
Fill(containers.ToList(), new List<Node>(), 0);

void Fill(List<Node> nodesLeft, List<Node> nodesUsed, int filled, int depth = 0)
{
    i++;
    //if (relevants.Sum(x => x.Value) + filled < volume) { return; }
    foreach (var item in nodesLeft)
    {
        if (depth < 4)
        {
            Console.WriteLine($"{item.Value} d {depth} {i}");
        }
        var newFilled = filled + item.Value;
        if (newFilled > volume)
        {
            continue;
        }

        var cloneUsed = nodesUsed.ToList();
        cloneUsed.Add(item);
        if (newFilled == volume)
        {
            combinations.Add(string.Join("|", cloneUsed.Select(x => x.GetHashCode()).OrderBy(x => x)));
            continue;
        }

        var cloneLeft = nodesLeft.Where(x => x != item).ToList();

        Fill(cloneUsed, cloneLeft, newFilled, depth + 1);
    }
}

result = combinations.Count;

timer.Stop();
Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();

class Node
{
    public int Value { get; set; }
}