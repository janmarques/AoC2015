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
//input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var xxxx = GetCombinations(new[] { new Node { Value = 1 }, new Node { Value = 2 }, new Node { Value = 2 }, new Node { Value = 3 }}).Select(x => x.ToList()).ToList();

var containers = input.Split(Environment.NewLine).Select(int.Parse).Select(x => new Node { Value = x }).ToList();
var volume = containers.Count == 5 ? 25 : 150;

var cache = Enumerable.Range(0, containers.Sum(x => x.Value) + 1).ToDictionary(x => x, x => new List<List<Node>>());
//var cacheKeyed = Enumerable.Range(0, volume).ToDictionary(x => x, x => new List<string>());
foreach (var container in containers)
{
    cache[container.Value].Add(new List<Node> { container });
}

for (int i = 1; i <= volume; i++)
{
    var relevant = cache.Where(x => x.Key < i && x.Value.Any()).SelectMany(x => x.Value.Select(y => (x.Key, y))).ToList();
    //var permutations2 = GetAllPermutations(relevant).ToList();
    var permutations = GetAllPermutations(relevant).Select(x => x.ToList()).Select(x => (value: x, hash: Hash(x.SelectMany(y => y.y)))).DistinctBy(x => x.hash).ToList();
    foreach (var permutation in permutations)
    {
        var sum = permutation.value.Sum(x => x.Key);
        cache[sum].Add(permutation.value.SelectMany(x => x.y).ToList());
    }

    if (i == 11)
    {

    }
}

string Hash(IEnumerable<Node> nodes) => string.Join("|", nodes.Select(x => x.GetHashCode()).OrderBy(x => x));

IEnumerable<IEnumerable<T>> GetAllPermutations<T>(IEnumerable<T> list)
{
    var count = list.Count();
    //if(count == 0) { yield break;}
    yield return list;
    for (int i = 2; i <= count; i++)
    {
        foreach (var item in GetPermutations(list, i).ToList())
        
        {
            yield return item;
        }
    }
}

IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
{
    if (length == 1) return list.Select(t => new T[] { t });

    return GetPermutations(list, length - 1)
        .SelectMany(t => list.Where(e => !t.Contains(e)),
            (t1, t2) => t1.Concat(new T[] { t2 }));
}

// https://stackoverflow.com/questions/64998630/get-all-combinations-of-liststring-where-order-doesnt-matter-and-minimum-of-2
IEnumerable<T[]> GetCombinations<T>(T[] source)
{
    for (var i = 0; i < (1 << source.Count()); i++)
        yield return source
           .Where((t, j) => (i & (1 << j)) != 0)
           .ToArray();

}
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