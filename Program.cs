using System.Net.Http.Headers;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;

var fullInput =
@"Al => ThF
Al => ThRnFAr
B => BCa
B => TiB
B => TiRnFAr
Ca => CaCa
Ca => PB
Ca => PRnFAr
Ca => SiRnFYFAr
Ca => SiRnMgAr
Ca => SiTh
F => CaF
F => PMg
F => SiAl
H => CRnAlAr
H => CRnFYFYFAr
H => CRnFYMgAr
H => CRnMgYFAr
H => HCa
H => NRnFYFAr
H => NRnMgAr
H => NTh
H => OB
H => ORnFAr
Mg => BF
Mg => TiMg
N => CRnFAr
N => HSi
O => CRnFYFAr
O => CRnMgAr
O => HP
O => NRnFAr
O => OTi
P => CaP
P => PTi
P => SiRnFAr
Si => CaSi
Th => ThCa
Ti => BP
Ti => TiTi
e => HF
e => NAl
e => OMg

CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";

var smallInput =
@"e => H
e => O
H => HO
H => OH
O => HH

HOHOHO";



//var replace = new Dictionary<string, string>()
//{
//    { "Al", "1" },
//    { "Ca", "2" },
//    { "Mg", "3" },
//    { "Si", "4" },
//    { "Th", "5" },
//    { "Ti", "6" },
//};

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = int.MaxValue;

//foreach (var item in replace)
//{
//    input = input.Replace(item.Key, item.Value);
//}

var lines = input.Split(Environment.NewLine);
var inversePairs = lines.SkipLast(2).Select(x => x.Split(" => ")).Select(x => (source: x[1], target: x[0])).GroupBy(x => x.target, x => x.source).ToDictionary(x => x.Key, x => x.ToList());
var pairs = lines.SkipLast(2).Select(x => x.Split(" => ")).Select(x => (source: x[0], target: x[1]));
var original = lines.Last();

var target = "e";
var molecules = new HashSet<string>() { original };

molecules = inversePairs.Where(x => x.Key != target).SelectMany(x => x.Value).ToHashSet();
var distances = inversePairs[target].ToDictionary(x => x, x => 1);

var xxx = new HashSet<int>();

var copy = original;
while (true)
{
    copy = original;
    int stuck = 0;
    var randomOrder = pairs.OrderByDescending(x => Guid.NewGuid()).ToList();
    while (copy != target)
    {
        var pair = randomOrder.FirstOrDefault(x => copy.Contains(x.target));
        if (pair == default)
        {
            goto end;
        }
        if (copy.Contains(pair.target))
        {
            copy = new Regex(pair.target).Replace(copy, pair.source, 1);
            stuck++;
        }
        if (copy == target)
        {
            xxx.Add(stuck);
            break;
        }
    }
end:;
}


while (molecules.Any())
{
    var newMolecules = molecules.ToHashSet();
    foreach (var molecule in molecules)
    {
    }
}

//var maxSteps = int.MaxValue;

//Discover("e", 0, 0);

//void Discover(string molecule, int steps, int cursor)
//{
//    if (molecule.Length > 30 && !target.StartsWith(molecule)) { return; }
//    if (steps > maxSteps) { return; }
//    if (molecule == target)
//    {
//        maxSteps = Math.Min(maxSteps, steps);
//        return;
//    }
//    var hasVariant = false;
//    var @char = molecule[cursor];
//    foreach (var item in pairs.Where(x => x.source == @char))
//    {
//        hasVariant = true;
//        var sb = new StringBuilder(molecule);
//        sb.Remove(cursor, 1);
//        sb.Insert(cursor, item.target);
//        Discover(sb.ToString(), steps+1, 0);
//    }
//    if (!hasVariant)
//    {
//        Discover(molecule, steps + 1, cursor + 1);
//    }
//}

//result = maxSteps;

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