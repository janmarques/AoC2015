var fullInput =
@"1
2
3
7
11
13
17
19
23
31
37
41
43
47
53
59
61
67
71
73
79
83
89
97
101
103
107
109
113";

var smallInput =
@"1
2
3
4
5
7
8
9
10
11";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0ul;


var sss = Product(new HashSet<ulong> { 11, 9 });

var numbers = input.Split(Environment.NewLine).Select(ulong.Parse).ToArray();
var sum = Sum(numbers);
if (sum % 3 != 0) { throw new Exception(); }
var target = sum / 3;

var qqq = Enumerable.Range(0,100000).Select(x => numbers.OrderBy(x => Guid.NewGuid()).Take(5)).Where(x => Sum(x) == target).ToList();

var groups = TryMakeGroup(new HashSet<ulong>(), 0).Take(100_000_000).Select(x => (set: x, count: x.Count, entanglement: Product(x), hash: Hash(x))).DistinctBy(x => x.hash).OrderBy(x => x.count).ThenBy(x => x.entanglement).ToList();
foreach (var item in groups)
{
    var forbidden = new HashSet<ulong>(item.set);
    for (int i = 0; i < 2; i++)
    {
        var compatible = groups.Where(x => forbidden.All(y => x.entanglement % y != 0)).FirstOrDefault();
        if (compatible == default) { goto next; }
        foreach (var item1 in compatible.set)
        {
            forbidden.Add(item1);
        }
    }
    break;
next:;
}

ulong Product(HashSet<ulong> set) => set.Aggregate(1ul, (x, y) => y = x * y);
string Hash(HashSet<ulong> set) => string.Join("|", set.OrderBy(x => x));
ulong Sum(IEnumerable<ulong> set) => set.Aggregate(0ul, (x, y) => y = x + y);

IEnumerable<HashSet<ulong>> TryMakeGroup(HashSet<ulong> used, ulong sum)
{
    foreach (var number in numbers)
    {
        if (used.Contains(number))
        {
            continue;
        }
        var newUsed = new HashSet<ulong>(used);
        newUsed.Add(number);
        var newSum = sum + number;
        if (newSum > target)
        {
        }
        else
        if (newSum == target)
        {
            yield return newUsed;
        }
        else
        {
            foreach (var item in TryMakeGroup(newUsed, newSum))
            {
                yield return item;
            }
        }
    }
}



timer.Stop();
Console.WriteLine(result); // 42093166160081598 too high
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();

void PrintGrid<T>(T[][] grid)
{
    for (int i = 0; i < grid.Length; i++)
    {
        for (int j = 0; j < grid[i].Length; j++)
        {
            Console.Write(grid[i][j]);
        }
        Console.WriteLine();
    }
}