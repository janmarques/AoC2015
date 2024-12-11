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
@"H => HO
H => OH
O => HH

HOH";



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
//input = fullInput;
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

var molecules = new HashSet<string>();

Discover(new StringBuilder(), 0);

void Discover(StringBuilder molecule, int cursor)
{
    if (cursor == original.Length - 1)
    {
        molecules.Add(molecule.ToString());
        return;
    }
    var hasVariant = false;
    var @char = original[cursor];
    foreach (var item in pairs.Where(x => x.source == @char))
    {
        hasVariant = true;
        var copy = new StringBuilder(molecule.Length);
        copy.Append(molecule);
        copy.Append(item.target);
        Discover(copy, cursor + 1);
    }
    if (!hasVariant)
    {
        molecule.Append(original[cursor]);
        Discover(molecule, cursor + 1);
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