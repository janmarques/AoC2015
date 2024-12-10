var fullInput =
@"Tristram to AlphaCentauri = 34
Tristram to Snowdin = 100
Tristram to Tambi = 63
Tristram to Faerun = 108
Tristram to Norrath = 111
Tristram to Straylight = 89
Tristram to Arbre = 132
AlphaCentauri to Snowdin = 4
AlphaCentauri to Tambi = 79
AlphaCentauri to Faerun = 44
AlphaCentauri to Norrath = 147
AlphaCentauri to Straylight = 133
AlphaCentauri to Arbre = 74
Snowdin to Tambi = 105
Snowdin to Faerun = 95
Snowdin to Norrath = 48
Snowdin to Straylight = 88
Snowdin to Arbre = 7
Tambi to Faerun = 68
Tambi to Norrath = 134
Tambi to Straylight = 107
Tambi to Arbre = 40
Faerun to Norrath = 11
Faerun to Straylight = 66
Faerun to Arbre = 144
Norrath to Straylight = 115
Norrath to Arbre = 135
Straylight to Arbre = 127";

var smallInput =
@"London to Dublin = 464
London to Belfast = 518
Dublin to Belfast = 141";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var distances = new Dictionary<(string, string), int>();

foreach (var line in input.Split(Environment.NewLine))
{
    var split = line.Split(' ');
    var key = (split[0], split[2]);
    var value = int.Parse(split[4]);
    distances.Add(key, value);
}
var cities = distances.Keys.SelectMany(key => new[] { key.Item1, key.Item2 }).Distinct().ToList();

var permutations = GetPermutations(cities, cities.Count).Select(x => x.ToArray()).ToList();
IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
{
    if (length == 1) return list.Select(t => new T[] { t });

    return GetPermutations(list, length - 1)
        .SelectMany(t => list.Where(e => !t.Contains(e)),
            (t1, t2) => t1.Concat(new T[] { t2 }));
}

result = permutations.Select(GetDistance).Min();
int GetDistance(string[] cities)
{
    var count = 0;
    for (var i = 0; i < cities.Length - 1; i++)
    {
        if (distances.ContainsKey((cities[i], cities[i + 1])))
        {
            count += distances[(cities[i], cities[i + 1])];
        }
        else
        {
            count += distances[(cities[i + 1], cities[i])];
        }
    }
    return count;
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