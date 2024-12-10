var fullInput =
@"";

var smallInput =
@"vzbxxzaa";

var smallest = "";

var x = Evaluate("abc".ToCharArray());

var input = smallInput;
//input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var input2 = input.ToCharArray();
while (true)
{
    if (Evaluate(input2))
    {
        break;
    }
    input2 = Increment(input2);
}

bool Evaluate(char[] input)
{
    var hasThree = false;
    var threeStack = new Stack<char>(3);

    var twoSet = new HashSet<char>();
    var twoPrevious = default(char);
    foreach (var item in input)
    {
        if (item == 'i' || item == 'o' || item == 'l')
        {
            return false;
        }
        if (!hasThree)
        {
            if (threeStack.TryPeek(out var previous))
            {
                if (previous + 1 == item)
                {
                    if (threeStack.Count == 2)
                    {
                        hasThree = true;
                    }
                    else
                    {
                        threeStack.Push(item);
                    }
                }
                else
                {
                    threeStack.Clear();
                    threeStack.Push(item);
                }
            }
            else
            {
                threeStack.Push(item);
            }
        }
        if (twoSet.Count < 2)
        {
            if (twoPrevious == item)
            {
                twoSet.Add(item);
            }
            else
            {
                twoPrevious = item;
            }
        }
    }
    return hasThree && twoSet.Count >= 2;
}

char[] Increment(char[] input)
{
    for (int i = input.Length - 1; i >= 0; i--)
    {
        input[i]++;
        if (input[i] > 'z')
        {
            input[i] = 'a';
        }
        else
        {
            break;
        }
    }
    return input;
}

timer.Stop();
Console.WriteLine(new string(input2));
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