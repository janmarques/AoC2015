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

var result = 0;

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

var staticSpells = new[] { "missile", "drain" };
var effects = new[] { ("shield", 6, 113), ("poison", 6, 173), ("recharge", 5, 229) };
var spells = staticSpells.Concat(effects.Select(x => x.Item1));

var someSpells = new List<string>();
for (int i = 0; i < 1000; i++)
{
    someSpells.Add(spells.OrderBy(x => Guid.NewGuid()).First());
}
//var me = new Player { Name = "me", Hp = 8, Damage = 5, Armor = 5 };
//var boss = new Player { Name = "boss", Hp = 12, Damage = 7, Armor = 2 };


Player CreateBoss() => new Player { Name = "boss", Hp = 14, Damage = 8 };
Player CreateMe() => new Player { Name = "me", Hp = 10, Mana = 250 };

someSpells = new List<string>() { "recharge", "shield", "drain", "poison", "missile" };
var xxx = Simulate(someSpells);

bool Simulate(List<string> chosenSpells)
{
    var boss = CreateBoss();
    var me = CreateMe();
    var activeEffects = new Dictionary<string, int>();
    foreach (var spell in chosenSpells)
    {
        void RunEffects()
        {
            me.Armor = 0;
            foreach (var activeEffect in activeEffects)
            {
                switch (activeEffect.Key)
                {
                    case "shield": me.Armor = 7; break;
                    case "poison": boss.Hp -= 3; break;
                    case "recharge": me.Mana += 101; break;
                    default: throw new Exception();
                }
                activeEffects[activeEffect.Key]--;
            }
            activeEffects = activeEffects.Where(x => x.Value > 0).ToDictionary();
        }

        RunEffects();
        if (boss.Hp <= 0)
        {
            return true;
        }
        var effect = effects.SingleOrDefault(x => x.Item1 == spell);
        if (effect != default)
        {
            if (activeEffects.ContainsKey(spell))
            {
                continue; // invalid random spell
            }
            else
            {
                activeEffects[spell] = effect.Item2;
                me.Mana -= effect.Item3;
            }
        }
        else if (spell == "missile")
        {
            me.Mana -= 53;
            boss.Hp -= 4;
        }
        else if (spell == "drain")
        {
            me.Mana -= 73;
            boss.Hp -= 2;
            me.Hp += 2;
        }
        else
        {
            throw new Exception();
        }

        if (boss.Hp <= 0)
        {
            return true;
        }


        RunEffects();
        if (boss.Hp <= 0)
        {
            return true;
        }
        me.Hp -= Math.Max(1, boss.Damage - me.Armor);


        if (me.Hp <= 0)
        {
            return false;
        }
    }
    throw new Exception();
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
    public int Mana { get; set; }
    public int Damage { get; set; }
    public int Armor { get; set; }
}