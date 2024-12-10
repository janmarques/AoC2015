var fullInput =
@"Sprinkles: capacity 2, durability 0, flavor -2, texture 0, calories 3
Butterscotch: capacity 0, durability 5, flavor -3, texture 0, calories 3
Chocolate: capacity 0, durability 0, flavor 5, texture -1, calories 8
Candy: capacity 0, durability -1, flavor 0, texture 5, calories 8";

var smallInput =
@"Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

var ingredients = input.Replace(",", "").Split(Environment.NewLine).Select(x => x.Split(' ')).Select(x => new[] { x[2], x[4], x[6], x[8], x[10] }).Select(x => x.Select(int.Parse).ToList()).ToList();

var proportionsList = Distribute(100, ingredients.Count).Select(x => x.ToList()).ToList();
var propertiesCount = ingredients.First().Count;

foreach (var proportions in proportionsList)
{
    var propertyDct = Enumerable.Range(0, propertiesCount).ToDictionary(x => x, x => new List<int>());
    for (var i = 0; i < proportions.Count; i++)
    {
        for (int j = 0; j < propertiesCount; j++)
        {
            propertyDct[j].Add(proportions[i] * ingredients[i][j]);
        }
    }
    if (propertyDct.Last().Value.Sum() != 500) { continue; }
    var product = propertyDct.SkipLast(1).Select(x => Math.Max(0, x.Value.Sum())).Aggregate(1, (x, y) => x * y);
    result = Math.Max(result, product);
}

IEnumerable<List<int>> Distribute(int items, int buckets)
{
    if (buckets == 1)
    {
        yield return new List<int>() { items };
        yield break;
    }
    for (int i = 0; i < items; i++)
    {
        foreach (var item in Distribute(items - i, buckets - 1))
        {
            var list = new List<int>() { i };
            list.AddRange(item);
            yield return list;
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