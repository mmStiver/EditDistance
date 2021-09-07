using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using EditDistance.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace EditDistance.Benchmark {
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class EditDistanceTest {
         
        [GlobalSetup]
        public void Setup()
        {
        }

        private static readonly List<string> states = new List<string>() {"Alabama","Alaska", "Arizona", "Arkansas", "California",	 "Colorado","Connecticut",	"Delaware", "Florida",
 "Georgia",	"Hawaii",	"Idaho", "Illinois",	"Indiana",	"Iowa", "Kansas", "Kentucky",	"Louisiana", "Maine","Maryland", "Massachusetts", "Michigan",	
 "Minnesota", "Mississippi" , "Missouri","Montana",	 "Nebraska","Nevada","New Hampshire", "New Jersey", "New Mexico",
            "New York" ,"North Dakota","North Carolina","Ohio","Oklahoma","Oregon","Pennsylvania","Rhode Island","South Carolina","South Dakota","Tennessee",
            "Texas", "Utah","Vermont","Virginia" ,"Washington" ,"West Virginia" ,"Wisconsin","Wyoming","District of Columbia,"};
        

        [Benchmark]
        public int DamerauLevenshteDistance()
        {
            foreach(var spelling in MisspelledStates())
                foreach(var s in states)
                    (new NewDamerauLevenshtein()).ComputeDistance(spelling, s);
               
            return -1;
        }
        [Benchmark]
        public int DamerauLevInMemory()
        {
            foreach(var spelling in MisspelledStates())
                foreach(var s in states)
                    (new NewDamerauLevenshtein()).ComputeDistance(spelling.AsSpan(), s.AsSpan());
               
            return -1;
        }
        [Benchmark]
        public int JaroWinklerStatesDistance()
        {
            foreach(var spelling in MisspelledStates())
                foreach(var s in states)
                    (new JaroWinkler()).ComputeDistance(spelling, s);
               
            return -1;
        }
        [Benchmark]
        public int JaroWinklerInMemory()
        {
            foreach(var spelling in MisspelledStates())
                foreach(var s in states)
                    (new JaroWinkler()).ComputeDistance(spelling.AsSpan(), s.AsSpan());
               
            return -1;
        }
        
        [Benchmark]
        public int QuickDamerauLevDistance()
        {
            foreach(var spelling in MisspelledStates())
                foreach(var s in states)
                    (new QuickDamerauLevenshtein()).ComputeDistance(spelling, s);
               
            return -1;
        }
        [Benchmark]
        public int QuickDamerauLevInMemory()
        {
            foreach(var spelling in MisspelledStates())
                foreach(var s in states)
                    (new QuickDamerauLevenshtein()).ComputeDistance(spelling.AsSpan(), s);
               
            return -1;
        }

        public IEnumerable<string> MisspelledStates() {
            yield return "Allabama"     ;
            yield return "Arizonia"     ;
            yield return "Arzinoa"      ;
            yield return "Calorado"     ;
            yield return "Colarado"     ;
            yield return "connecticutt" ;
            yield return "conneticutt"  ;
            yield return "connetticut"  ;
            yield return "connetticutt" ;
            yield return "conneticut"   ;
            yield return "Floryda"      ;
            yield return "Florda"       ;
            yield return "Flordia"      ;
            yield return "Hawai"        ;
            yield return "Howaii"       ;
            yield return "Hawii"        ;
            yield return "Hawaai"       ;
            yield return "Idahoe"       ;
            yield return "kentuky"      ;
            yield return "kentucy"      ;
            yield return "Louiseiana"   ;
            yield return "main"         ;
            yield return "miane"        ;
            yield return "Massachussetts";
            yield return "Masachusetts";
            yield return "Masachusets";
            yield return "Massachussets";
            yield return "Mississipi";
            yield return "Misouri"   ;
            yield return "Missisipi" ;
            yield return "Missoury"  ;
            yield return "Missisippi";
            yield return "Oiho"      ;
            yield return "Okalahoma" ;
            yield return "Pensylvania";
            yield return "Tennesee";
            yield return "Tennisse";
            yield return "Tennissee";
            yield return "Whyoming";
            yield return "Wioming";
            yield return "Wisconson";    
        }
    }
}