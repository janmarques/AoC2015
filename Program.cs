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

var result = 0l;

var sss = Entanglement(new HashSet<long> { 11, 9 });

var numbers = input.Split(Environment.NewLine).Select(long.Parse).ToArray();
if (numbers.Sum() % 3 != 0) { throw new Exception(); }
var target = numbers.Sum() / 3;

var groups = TryMakeGroup(new HashSet<long>(), 0).Take(1000).Select(x => (set: x, count: x.Count, entanglement: Entanglement(x))).DistinctBy(x => x.entanglement).ToList();

foreach (var item in groups.OrderBy(x => x.count).ThenBy(x => x.entanglement))
{
    var compatible = groups.Where(x => item.set.All(y => x.entanglement % y == 0)).Count();
    if (compatible == 2)
    {
        result = item.entanglement;
        break;
    }
}

long Entanglement(HashSet<long> set) => set.Aggregate(1l, (x, y) => y = x * y);

IEnumerable<HashSet<long>> TryMakeGroup(HashSet<long> used, long sum)
{
    if(sum > target) { yield break; }
    foreach (var number in numbers)
    {
        if (used.Contains(number))
        {
            continue;
        }
        var newUsed = new HashSet<long>(used);
        newUsed.Add(number);
        var newSum = sum + number;
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
Console.WriteLine(result);
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