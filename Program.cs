var fullInput =
@"";

var smallInput =
@"";

var smallest = "";

var input = smallInput;
//input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0l;

foreach (var line in input.Split(Environment.NewLine))
{

}

var sequenceNumber = GetSequenceNumber(2981, 3075);
result = 20151125;
for (int i = 0; i < sequenceNumber-1; i++)
{
    result = (result * 252533l) % 33554393l;
}


int GetSequenceNumber(int row, int column)
{
    var columnStart = 0;
    for (int i = 1; i <= column; i++)
    {
        columnStart += i;
    }

    var value = columnStart;
    for (int j = 0; j < row - 1; j++)
    {
        value += j + column;
    }

    return value;
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