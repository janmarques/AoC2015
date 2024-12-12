using System.Linq;

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



var numbers = input.Split(Environment.NewLine).Select(int.Parse).ToArray();
var sum = numbers.Sum();
if (sum % 4 != 0) { throw new Exception(); }
var target = sum / 4;
int ss = 0;
int take = 100000;

var groupsAA = TryMakeGroup(new List<int>(), numbers.OrderByDescending(x => x).ToList(), 0);
var groups = Sanizite(take, groupsAA);

foreach (var grp in groups)
{
    var others1 = Sanizite(100, TryMakeGroup(new List<int>(), numbers.Except(grp.set).OrderByDescending(x => x).ToList(), 0));

    foreach (var other1 in others1)
    {
        var others2 = Sanizite(100, TryMakeGroup(new List<int>(), numbers.Except(grp.set).Except(other1.set).OrderByDescending(x => x).ToList(), 0));

        foreach (var other2 in others2)
        {
            var others3 = Sanizite(100, TryMakeGroup(new List<int>(), numbers.Except(grp.set).Except(other1.set).Except(other2.set).OrderByDescending(x => x).ToList(), 0));

            if (others3.Any())
            {
                result = grp.entanglement;
                goto end;
            }
        }
    }
}
end:;

ulong Product(List<int> set) => set.Aggregate(1ul, (x, y) => x = x * (ulong)y);
string Hash(List<int> set) => string.Join("|", set.OrderBy(x => x));
int Sum(IEnumerable<int> set) => set.Aggregate(0, (x, y) => y = x + y);


IEnumerable<List<int>> TryMakeGroup(List<int> used, List<int> left, int sum)
{
    foreach (var number in left)
    {
        var newSum = sum + number;
        var newUsed = used.Concat(new[] { number }).ToList();
        if (newSum > target)
        {
            continue;
        }
        else if (newSum == target)
        {
            ss++;
            if (ss % 10000 == 0)
            {
                Console.WriteLine((double)ss / take);
            }
            yield return newUsed;
        }
        else
        {
            var newLeft = left.Where(x => x != number && x <= newSum).ToList();
            foreach (var item in TryMakeGroup(newUsed, newLeft, newSum))
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

List<(List<int> set, int count, ulong entanglement, string hash)> Sanizite(int take, IEnumerable<List<int>> groupsAA)
{
    return groupsAA.Take(take)
    .Select(x => (set: x, count: x.Count, entanglement: Product(x), hash: Hash(x)))
    .DistinctBy(x => x.hash)
    .OrderBy(x => x.count)
    .ThenBy(x => x.entanglement)
    .ToList();
}