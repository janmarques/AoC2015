//using System.Text;

//var fullInput =
//@"1113222113";

//var smallInput =
//@"111221";

//var smallest = "";

//var input = smallInput;
//input = fullInput;
////input = smallest;
//var timer = System.Diagnostics.Stopwatch.StartNew();

//var result = 0;

//var input2 = input.ToList();
//for (int j = 0; j < 50; j++)
//{
//    var targetString = new List<char>();
//    for (int i = 0; i < input2.Count; i++)
//    {
//        var character = input2[i];
//        var chunk = input2.Skip(i).TakeWhile(x => x == input2[i]);
//        var count = chunk.Count();
//        i += count - 1;
//        targetString.AddRange(count.ToString());
//        targetString.Add(character);
//    }
//    input2 = targetString.ToList();

//    Console.WriteLine($"{j} {timer.ElapsedMilliseconds} {input2.Count}");
//    timer.Restart();
//}

//result = input2.Count;
//timer.Stop();
//Console.WriteLine(result);
//Console.WriteLine(timer.ElapsedMilliseconds + "ms");
//Console.ReadLine();

//void PrintGrid<T>(T[][] grid)
//{
//    for (int i = 0; i < grid.Length; i++)
//    {
//        for (int j = 0; j < grid[i].Length; j++)
//        {
//            Console.Write(grid[i][j]);
//        }
//        Console.WriteLine();
//    }
//}