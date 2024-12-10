var fullInput =
@"1113222113";

var smallInput =
@"111221";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

for (int j = 0; j < 40; j++)
{
    var targetString = "";
    for (int i = 0; i < input.Length; i++)
    {
        var character = input[i];
        var chunk = input.Skip(i).TakeWhile(x => x == input[i]);
        var count = chunk.Count();
        i += count - 1;
        targetString += count.ToString();
        targetString += character.ToString();
    }
    input = targetString;
}

result = input.Length;
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