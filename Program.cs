var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

Item ToItem(string arg)
{
    var split = arg.Split("  ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
    return new Item(split.ElementAt(1), split.ElementAt(2), split.ElementAt(3));
}

var staticSpells = new[] { "missile", "drain" };
var effects = new[] { ("shield", 6), ("poison", 6), ("recharge", 5) };
var spells = staticSpells.Concat(effects.Select(x => x.Item1));

var costs = new Dictionary<string, int>()
{
    { "missile", 53},
    { "drain", 73},
    { "shield", 113},
    { "poison", 173},
    { "recharge", 229},
};




Player CreateMe() => new Player { Name = "me", Hp = 50, Mana = 500 };
Player CreateBoss() => new Player { Name = "boss", Hp = 103, Damage = 9 };

while (true)
{
    var working = new List<string>()
    {
"shield",
"recharge",
"poison",
"shield",
"recharge",
"poison",
"shield",
"recharge",
"poison",
"shield",
"recharge",
"poison",
"shield",
"recharge",
"poison",
"shield",
"missile",
"poison",
"missile",
"missile"

    };

    working.Reverse();
    var someSpells = new Stack<string>(working);
    //for (int i = 0; i < 100; i++)
    //{
    //    someSpells.Push(spells.OrderBy(x => Guid.NewGuid()).First());
    //}


    var copy = someSpells.ToList();
    var boss = CreateBoss();
    var me = CreateMe();
    var xxx = Simulate(someSpells, me, boss);
    if (boss.Hp < 15)
    {

        Console.WriteLine(me);
        Console.WriteLine(boss);
        Console.WriteLine($"{result} {string.Join("|", copy.Take(result))}");
        Console.WriteLine();
    }
    if (xxx)
    {
        break;
    }
}

bool Simulate(Stack<string> chosenSpells, Player me, Player boss)
{
    result = 0;
    var activeEffects = new Dictionary<string, int>();
    while (true)
    {
        result++;
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
        if (me.Mana < costs.Min(x => x.Value))
        {
            return false;
        }
        string spell;
        do
        {
            spell = chosenSpells.Pop();
        } while (costs[spell] > me.Mana || activeEffects.ContainsKey(spell));
        Console.WriteLine($"\"{spell}\",");

        me.Mana -= costs[spell];

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
            }
        }
        else if (spell == "missile")
        {
            boss.Hp -= 4;
        }
        else if (spell == "drain")
        {
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

    public override string ToString() => $"{Name} {Hp}hp {Mana}m";
}