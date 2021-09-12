using EditDistance.Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EditDistance {
    public class Program {
        private static readonly List<string> states = new List<string>() {"Alabama","Alaska", "Arizona", "Arkansas", "California",	 "Colorado","Connecticut",	"Delaware", "Florida",
 "Georgia",	"Hawaii",	"Idaho", "Illinois",	"Indiana",	"Iowa", "Kansas", "Kentucky",	"Louisiana", "Maine","Maryland", "Massachusetts", "Michigan",	
 "Minnesota", "Mississippi" , "Missouri","Montana",	 "Nebraska","Nevada","New Hampshire", "New Jersey", "New Mexico",
            "New York" ,"North Dakota","North Carolina","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee",
            "Texas", "Utah","Vermont","Virginia" ,"Washington" ,"West Virginia" ,"Wisconsin","Wyoming","District of Columbia"};
        
        public static IEnumerable<string> MisspelledStates() {
            yield return "Allabama"     ;yield return "Arizonia"     ;            yield return "Arzinoa"      ;
            yield return "Calorado"     ;yield return "Colarado"     ;            yield return "connecticutt" ;
            yield return "conneticutt"  ;yield return "connetticut"  ;            yield return "connetticutt" ;
            yield return "conneticut"   ;yield return "Floryda"      ;            yield return "Florda"       ;
            yield return "Flordia"      ;yield return "Hawai"        ;            yield return "Howaii"       ;
            yield return "Hawii"        ;yield return "Hawaai"       ;            yield return "Idahoe"       ;
            yield return "kentuky"      ;yield return "kentucy"      ;            yield return "Louiseiana"   ;
            yield return "main"         ;yield return "miane"        ;            yield return "Massachussetts";
            yield return "Masachusetts";yield return "Masachusets";            yield return "Massachussets";
            yield return "Mississipi";yield return "Misouri"   ;            yield return "Missisipi" ;
            yield return "Missoury"  ;yield return "Missisippi";            yield return "Oiho"      ;
            yield return "Okalahoma" ;yield return "Pensylvania";            yield return "Tennesee";
            yield return "Tennisse";yield return "Tennissee";            yield return "Whyoming";
            yield return "Wioming";yield return "Wisconson";
        }
                   
        public static void Main(string[] args) {
            Console.WriteLine("Search String: ");
            var input = Console.ReadLine();

            //var results = new List<(string,string, int)>();
            var results = new List<(string,string, double)>();
            IEditDistance<int> damLev = new QuickDamerauLevenshtein();
            IEditDistance<double> jaroWinkler = new JaroWinkler();
            foreach( var state in states) {
                results.Add(("jaroWinkler", state, jaroWinkler.ComputeDistance(input, state)));
            }
           
            Console.WriteLine();
            Console.WriteLine("Closest Match:");
            foreach(var r in results.Where(state => state.Item3 < 5).OrderBy(r => r.Item3).Take(5))
                Console.WriteLine("{0} - {1} - {2}", r.Item1, r.Item2, r.Item3.ToString());
            
        }
    }
}

