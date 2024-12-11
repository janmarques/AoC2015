var fullInput =
@"";

var smallInput =
@"Weapons:    Cost  Damage  Armor
Dagger        8     4       0
Shortsword   10     5       0
Warhammer    25     6       0
Longsword    40     7       0
Greataxe     74     8       0

Armor:      Cost  Damage  Armor
Leather      13     0       1
Chainmail    31     0       2
Splintmail   53     0       3
Bandedmail   75     0       4
Platemail   102     0       5

Rings:      Cost  Damage  Armor
Damage +1    25     1       0
Damage +2    50     2       0
Damage +3   100     3       0
Defense +1   20     0       1
Defense +2   40     0       2
Defense +3   80     0       3";

var smallest = "";

var input = smallInput;
//input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = int.MaxValue;

var lines = input.Split(Environment.NewLine);
var weapons = lines[1..6].Select(ToItem).ToList();
var armors = lines[8..13].Select(ToItem).ToList();
armors.Add(new Item(0, 0, 0));
var rings = lines[15..].Select(ToItem).ToList();
rings.Add(new Item(0, 0, 0));
rings.Add(new Item(0, 0, 0));

Item ToItem(string arg)
{
    var split = arg.Split("  ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
    return new Item(split.ElementAt(1), split.ElementAt(2), split.ElementAt(3));
}

//var me = new Player { Name = "me", Hp = 8, Damage = 5, Armor = 5 };
//var boss = new Player { Name = "boss", Hp = 12, Damage = 7, Armor = 2 };


Player CreateBoss() => new Player { Name = "boss", Hp = 103, Damage = 9, Armor = 2 };
Player CreateMe() => new Player { Name = "me", Hp = 100, Damage = 0, Armor = 0 };

foreach (var weapon in weapons)
    foreach (var armor in armors)
        foreach (var ring1 in rings)
            foreach (var ring2 in rings)
            {
                if (ring1 == ring2) { continue; }
                var items = new[] { weapon, armor, ring1, ring2 };
                var boss = CreateBoss();
                var me = CreateMe();
                me.Armor += items.Sum(x => x.armor);
                me.Damage += items.Sum(x => x.damage);
                var cost = items.Sum(x => x.cost);
                var win = SimulateFight(me, boss);
                if (win)
                {
                    result = Math.Min(cost, result);
                }
            }


bool SimulateFight(Player me, Player boss)
{
    while (true)
    {
        Attack(me, boss);
        if (boss.Hp <= 0)
        {
            return true;
        }
        Attack(boss, me);
        if (me.Hp <= 0)
        {
            return false;
        }
    }
}

void Attack(Player attacker, Player victim)
{
    victim.Hp -= Math.Max(1, attacker.Damage - victim.Armor);
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

record Item(int cost, int damage, int armor);

class Player
{
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Armor { get; set; }
    public int Damage { get; set; }
}