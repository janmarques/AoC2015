using System.Runtime.ExceptionServices;

var fullInput =
@"";

var smallInput =
@"";

var smallest = "";

var input = smallInput;
//input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

foreach (var line in input.Split(Environment.NewLine))
{

}

// 750960 too high
int i = 665280;
while (i < 750960)
{
    var xx = PresentsForHouse(i);
    if (xx > 29000000)
    {
        result = i;
        break;
    }
    i++;
}

int PresentsForHouse(int number)
{
    return GetFactors(number).Sum() * 10;
}

IEnumerable<int> GetFactors(int number)
{
    for (int i = 1; i <= number / 2; i++)
    {
        if (number % i == 0) { yield return i; }
    }
    yield return number;
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