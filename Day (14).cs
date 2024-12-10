//var fullInput =
//@"";

//var smallInput =
//@"Vixen can fly 8 km/s for 8 seconds, but then must rest for 53 seconds.
//Blitzen can fly 13 km/s for 4 seconds, but then must rest for 49 seconds.
//Rudolph can fly 20 km/s for 7 seconds, but then must rest for 132 seconds.
//Cupid can fly 12 km/s for 4 seconds, but then must rest for 43 seconds.
//Donner can fly 9 km/s for 5 seconds, but then must rest for 38 seconds.
//Dasher can fly 10 km/s for 4 seconds, but then must rest for 37 seconds.
//Comet can fly 3 km/s for 37 seconds, but then must rest for 76 seconds.
//Prancer can fly 9 km/s for 12 seconds, but then must rest for 97 seconds.
//Dancer can fly 37 km/s for 1 seconds, but then must rest for 36 seconds.";

//var smallest = "";

//var input = smallInput;
////input = fullInput;
////input = smallest;
//var timer = System.Diagnostics.Stopwatch.StartNew();

//var result = 0;

//var reindeers = new Dictionary<(string name, int speed, int duration, int rest), int>();
//var points = new Dictionary<(string name, int speed, int duration, int rest), int>();

//foreach (var line in input.Replace(" can fly", "").Replace(" km/s for", "").Replace(" seconds, but then must rest for", "").Replace(" seconds.", "").Split(Environment.NewLine))
//{
//    var split = line.Split(' ');
//    var name = split[0];
//    var numbers = split.Skip(1).Select(int.Parse).ToArray();
//    var key = (name, numbers[0], numbers[1], numbers[2]);
//    reindeers.Add(key, 0);
//    points.Add(key, 0);
//}

//for (int i = 0; i < 2503; i++)
//{
//    foreach (var reindeer in reindeers)
//    {
//        var key = reindeer.Key;
//        var isFlying = i % (key.duration + key.rest) < key.duration;
//        if (isFlying)
//        {
//            reindeers[reindeer.Key] += key.speed;
//        }
//    }

//    var furthest = reindeers.Max(x => x.Value);
//    foreach (var item in reindeers.Where(x => x.Value == furthest))
//    {
//        points[item.Key]++;
//    }
//}

//result = points.Max(x => x.Value);

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