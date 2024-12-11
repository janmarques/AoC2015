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

var xxxx = GetCombinations(new List<Node> { new Node { Value = 1 }, new Node { Value = 2 }, new Node { Value = 2 }, new Node { Value = 3 } }).Where(x => x.Length > 1).ToList();

var containers = input.Split(Environment.NewLine).Select(int.Parse).Select(x => new Node { Value = x }).ToList();
var volume = containers.Count == 5 ? 25 : 150;

var cache = Enumerable.Range(0, 1000).ToDictionary(x => x, x => new List<List<Node>>());
var cacheKeyed = Enumerable.Range(0, 1000).ToDictionary(x => x, x => new HashSet<string>());
foreach (var container in containers)
{
    cache[container.Value].Add(new List<Node> { container });
}

for (int i = 150; i <= volume; i++)
{
    var relevant = cache.Where(x => x.Key < i && x.Value.Any()).SelectMany(x => x.Value.Select(y => (x.Key, y))).ToList();
    //var permutations2 = GetAllPermutations(relevant).ToList();
    var permutations = GetCombinations(relevant).Where(x => x.Length > 1).ToList();
    foreach (var combination in permutations)
    {
        if (combination.SelectMany(x => x.y).Distinct().Count() != combination.SelectMany(x => x.y).Count()) { continue; }
        var sum = combination.Sum(x => x.Key);
        var lst = combination.SelectMany(x => x.y).ToList();
        var hash = Hash(lst);
        if (!cacheKeyed[sum].Contains(hash))
        {
            cacheKeyed[sum].Add(hash);
            cache[sum].Add(combination.SelectMany(x => x.y).ToList());
        }
    }

    Console.WriteLine(i);
    if (i == 11)
    {

    }
}

string Hash(IEnumerable<Node> nodes) => string.Join("|", nodes.Select(x => x.GetHashCode()).OrderBy(x => x));


// https://stackoverflow.com/questions/64998630/get-all-combinations-of-liststring-where-order-doesnt-matter-and-minimum-of-2
IEnumerable<T[]> GetCombinations<T>(List<T> source)
{
    for (var i = 0; i < (1 << source.Count); i++)
        yield return source
           .Where((t, j) => (i & (1 << j)) != 0)
           .ToArray();

}

var minContainers = cache[volume].Min(x => x.Count);
result = cache[volume].Count(x => x.Count == minContainers);
timer.Stop();
Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();

class Node
{
    public int Value { get; set; }
    public override string ToString()
    {
        return Value.ToString();
    }
}