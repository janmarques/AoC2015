//using System.Net.Http.Headers;
//using System.Runtime.ExceptionServices;
//using System.Text;
//using System.Text.RegularExpressions;

//var fullInput =
//@"Al => ThF
//Al => ThRnFAr
//B => BCa
//B => TiB
//B => TiRnFAr
//Ca => CaCa
//Ca => PB
//Ca => PRnFAr
//Ca => SiRnFYFAr
//Ca => SiRnMgAr
//Ca => SiTh
//F => CaF
//F => PMg
//F => SiAl
//H => CRnAlAr
//H => CRnFYFYFAr
//H => CRnFYMgAr
//H => CRnMgYFAr
//H => HCa
//H => NRnFYFAr
//H => NRnMgAr
//H => NTh
//H => OB
//H => ORnFAr
//Mg => BF
//Mg => TiMg
//N => CRnFAr
//N => HSi
//O => CRnFYFAr
//O => CRnMgAr
//O => HP
//O => NRnFAr
//O => OTi
//P => CaP
//P => PTi
//P => SiRnFAr
//Si => CaSi
//Th => ThCa
//Ti => BP
//Ti => TiTi
//e => HF
//e => NAl
//e => OMg

//CRnCaSiRnBSiRnFArTiBPTiTiBFArPBCaSiThSiRnTiBPBPMgArCaSiRnTiMgArCaSiThCaSiRnFArRnSiRnFArTiTiBFArCaCaSiRnSiThCaCaSiRnMgArFYSiRnFYCaFArSiThCaSiThPBPTiMgArCaPRnSiAlArPBCaCaSiRnFYSiThCaRnFArArCaCaSiRnPBSiRnFArMgYCaCaCaCaSiThCaCaSiAlArCaCaSiRnPBSiAlArBCaCaCaCaSiThCaPBSiThPBPBCaSiRnFYFArSiThCaSiRnFArBCaCaSiRnFYFArSiThCaPBSiThCaSiRnPMgArRnFArPTiBCaPRnFArCaCaCaCaSiRnCaCaSiRnFYFArFArBCaSiThFArThSiThSiRnTiRnPMgArFArCaSiThCaPBCaSiRnBFArCaCaPRnCaCaPMgArSiRnFYFArCaSiThRnPBPMgAr";

//var smallInput =
//@"e => H
//e => O
//H => HO
//H => OH
//O => HH

//HOHOHO";



////var replace = new Dictionary<string, string>()
////{
////    { "Al", "1" },
////    { "Ca", "2" },
////    { "Mg", "3" },
////    { "Si", "4" },
////    { "Th", "5" },
////    { "Ti", "6" },
////};

//var smallest = "";

//var input = smallInput;
//input = fullInput;
////input = smallest;
//var timer = System.Diagnostics.Stopwatch.StartNew();

//var result = int.MaxValue;


//var lines = input.Split(Environment.NewLine);
//var pairs = lines.SkipLast(2).Select(x => x.Split(" => ")).Select(x => (source: x[0], target: x[1]));
//var original = lines.Last();

//var target = "e";


//var copy = original;
//var i = 0;
//while (true)
//{
//    copy = original;
//    int stuck = 0;
//    var randomOrder = pairs.OrderByDescending(x => Guid.NewGuid()).ToList();
//    i++;
//    while (copy != target)
//    {
//        var pair = randomOrder.FirstOrDefault(x => copy.Contains(x.target));
//        if (pair == default)
//        {
//            goto next;
//        }
//        if (copy.Contains(pair.target))
//        {
//            copy = new Regex(pair.target).Replace(copy, pair.source, 1);
//            stuck++;
//        }
//        if (copy == target)
//        {
//            result = stuck;
//            goto end;
//        }
//    }
//next:;
//}
//end:;


//timer.Stop();
//Console.WriteLine(result);
//Console.WriteLine(timer.ElapsedMilliseconds + "ms");
//Console.ReadLine();

