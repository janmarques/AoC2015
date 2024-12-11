using System.Net.Http.Headers;
using System.Text;

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



var replace = new Dictionary<string, string>()
{
    { "Al", "1" },
    { "Ca", "2" },
    { "Mg", "3" },
    { "Si", "4" },
    { "Th", "5" },
    { "Ti", "6" },
};

var smallest = "";

var input = smallInput;
input = fullInput;
//input = smallest;
var timer = System.Diagnostics.Stopwatch.StartNew();

var result = 0;

foreach (var item in replace)
{
    input = input.Replace(item.Key, item.Value);
}

var lines = input.Split(Environment.NewLine);
var pairs = lines.SkipLast(2).Select(x => x.Split(" => ")).Select(x => (source: x[0][0], target: x[1])).ToArray();
var original = lines.Last();

var molecules = new HashSet<string>() { "e" };

while (true)
{
    var newMolecules = new HashSet<string>();

    foreach (var molecule in molecules)
    {
        foreach (var pair in pairs)
        {
            foreach (var item in molecule.Select((x, i) => (x, i)).Where((x) => x.x == pair.source))
            {
                var sb = new StringBuilder(molecule);
                sb.Remove(item.i, 1);
                sb.Insert(item.i, pair.target);
                newMolecules.Add(sb.ToString());
            }
        }
    }
    result++;
    molecules = newMolecules;
    if (molecules.Contains(original))
    {
        break;
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