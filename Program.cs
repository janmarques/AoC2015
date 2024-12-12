using System.ComponentModel;
using System.Security.Cryptography;

var timer = System.Diagnostics.Stopwatch.StartNew();

var result = int.MaxValue;

var costs = new Dictionary<string, int>()
{
    { "missile", 53},
    { "drain", 73},
    { "shield", 113},
    { "poison", 173},
    { "recharge", 229},
};

var pqSet = new HashSet<(string spell, int myHp, int myMana, int bossHp, int shieldTurns, int poisonTurns, int rechargeTurns, int manaSpent)>();

var pq = new PriorityQueue<(string spell, int myHp, int myMana, int bossHp, int shieldTurns, int poisonTurns, int rechargeTurns, int manaSpent), int>();
int BOSS_DAMAGE = 8;
foreach (var item in costs.Keys)
{
    var startState = (item, 50, 500, 55, 0, 0, 0, 0);
    //var startState = (item, 10, 250, 14, 0, 0, 0, 0);
    pq.Enqueue(startState, 0);
}


int i = 0;
while (pq.Count > 0)
{
    var (spell, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent) = pq.Dequeue();
    i++;
    if (i % 100000 == 0)
    {
        Console.WriteLine($"pq {pq.Count} state {manaSpent}");
    }
    var (simResult, myHpResult, myManaResult, bossHpResult, shieldTurnsResult, poisonTurnsResult, rechargeTurnsResult, manaSpentResult) = Simulate(spell, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);
    if (!simResult.HasValue)
    {
        if (manaSpentResult > result) { continue; }
        foreach (var item in costs.Keys)
        {
            switch (item)
            {
                case "shield":
                    if (shieldTurnsResult > 0) { continue; }
                    break;
                case "poison":
                    if (poisonTurnsResult > 0) { continue; }
                    break;
                case "recharge":
                    if (rechargeTurnsResult > 0) { continue; }
                    break;
            }
            if (!pqSet.Contains((item, myHpResult, myManaResult, bossHpResult, shieldTurnsResult, poisonTurnsResult, rechargeTurnsResult, manaSpentResult)))
            {
                pq.Enqueue((item, myHpResult, myManaResult, bossHpResult, shieldTurnsResult, poisonTurnsResult, rechargeTurnsResult, manaSpentResult), manaSpentResult);
            }
        }
    }
    else if (simResult.Value)
    {
        // Won
        result = Math.Min(result, manaSpentResult);
        //break;
    }
    else
    {
        // Lost / impossible
    }
}

(bool? won, int myHp, int myMana, int bossHp, int shieldTurns, int poisonTurns, int rechargeTurns, int manaSpent) Simulate(string spell, int myHp, int myMana, int bossHp, int shieldTurns, int poisonTurns, int rechargeTurns, int manaSpent)
{
    var armor = 0;
    void RunEffects()
    {
        armor = 0;
        if (shieldTurns > 0)
        {
            armor = 7;
            shieldTurns--;
        }
        if (poisonTurns > 0)
        {
            bossHp -= 3;
            poisonTurns--;
        }
        if (rechargeTurns > 0)
        {
            myMana += 101;
            rechargeTurns--;
        }
    }

    myHp -= 1;

    if (myHp <= 0)
    {
        return (false, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);
    }


    if (costs[spell] > myMana)
    {
        return (false, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);
    }

    switch (spell)
    {
        case "missile":
            bossHp -= 4;
            break;
        case "drain":
            bossHp -= 2;
            myHp += 2;
            break;
        case "shield":
            if (shieldTurns > 1) { return (false, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent); }
            shieldTurns += 6;
            break;
        case "poison":
            if (poisonTurns > 1) { return (false, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent); }
            poisonTurns += 6;
            break;
        case "recharge":
            if (rechargeTurns > 1) { return (false, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent); }
            rechargeTurns += 5;
            break;
        default: throw new Exception();
    }

    myMana -= costs[spell];
    manaSpent += costs[spell];

    if (bossHp <= 0)
    {
        return (true, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);

    }

    RunEffects();
    if (bossHp <= 0)
    {
        return (true, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);
    }
    myHp -= Math.Max(1, BOSS_DAMAGE - armor);

    if (myHp <= 0)
    {
        return (false, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);
    }

    RunEffects();
    if (bossHp <= 0)
    {
        return (true, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);
    }

   
    return (null, myHp, myMana, bossHp, shieldTurns, poisonTurns, rechargeTurns, manaSpent);
}

timer.Stop();
Console.WriteLine(result);
Console.WriteLine(timer.ElapsedMilliseconds + "ms");
Console.ReadLine();