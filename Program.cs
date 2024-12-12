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
//input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var numbers = input.Split(Environment.NewLine).Select(int.Parse).ToArray();

if (numbers.Sum() % 3 != 0) { throw new Exception(); }
var target = numbers.Sum() / 3;

var minNumbersPerGroup = 0;
while (true)
{
    minNumbersPerGroup++;
    if (numbers.TakeLast(minNumbersPerGroup).Sum() >= target)
    {
        break;
    }
}

while (true)
{
    var i = minNumbersPerGroup;
    var selected = new HashSet<int>();  
    do
    {

        selected.Add(i);
        i--;
    } while (i >= 0);
}
foreach (var line in input.Split(Environment.NewLine))
{

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