var fullInput =
@"jio a, +16
inc a
inc a
tpl a
tpl a
tpl a
inc a
inc a
tpl a
inc a
inc a
tpl a
tpl a
tpl a
inc a
jmp +23
tpl a
inc a
inc a
tpl a
inc a
inc a
tpl a
tpl a
inc a
inc a
tpl a
inc a
tpl a
inc a
tpl a
inc a
inc a
tpl a
inc a
tpl a
tpl a
inc a
jio a, +8
inc b
jie a, +4
tpl a
inc a
jmp +2
hlf a
jmp -7";

var smallInput =
@"inc a
jio a, +2
tpl a
inc a";

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var registers = new Dictionary<char, uint>()
{
    { 'a', 1 },
    { 'b', 0 }
};
uint result = 0;
var lines = input.Replace(",", "").Split(Environment.NewLine).Select(x => x.Split(' ')).ToArray();
for (int i = 0; i < lines.Length; i++)
{
    var line = lines[i];
    var op = line[0];
    var b = line[1];
    char bChar() => b.Single();
    int bInt() => int.Parse(b);
    int cInt() => int.Parse(line[2]);

    switch (op)
    {
        case "hlf": registers[bChar()] /= 2; break;
        case "tpl": registers[bChar()] *= 3; break;
        case "inc": registers[bChar()] += 1; break;
        case "jmp": i += bInt() - 1; break;
        case "jie": if (registers[bChar()] % 2 == 0) { i += cInt() - 1; } break;
        case "jio": if (registers[bChar()] == 1) { i += cInt() - 1; } break;
        default:
            throw new Exception();
            break;
    }
}

result = registers['b'];

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