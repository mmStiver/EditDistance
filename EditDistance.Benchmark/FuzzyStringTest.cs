using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using EditDistance.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace EditDistance.Benchmark {
    [SimpleJob(RuntimeMoniker.NetCoreApp31)]
    [MemoryDiagnoser]
    public class FuzzyStringTest {
         
        [GlobalSetup]
        public void Setup()
        {
        }

        [Benchmark]
        [ArgumentsSource(nameof(Words))]
        public int ComputeDistance((string, string) words)
        {
            (string source, string target) = words;
                searchDistance.ComputeDistance(source, target);
            return -1;
        }

        [Benchmark]
        [ArgumentsSource(nameof(Words))]
        public int ComputeSimilarity((string, string) words)
        {
            (string source, string target) = words;
                searchString.ComputeDistance(source, target);
            return -1;
        }

        [ParamsSource(nameof(iAlgorithms))]
        public IEditDistance<int> searchDistance;

        public IEnumerable<IEditDistance<int>> iAlgorithms() {
            yield return new SimpleLevenshtein();
            yield return new NewDamerauLevenshtein();
            yield return new QuickDamerauLevenshtein();
        }

        [ParamsSource(nameof(dAlgorithms))]
        public IEditDistance<double> searchString;

        public IEnumerable<IEditDistance<double>> dAlgorithms() {
            yield return new JaroWinkler();
        }

        public IEnumerable<(string, string)> Words() {
            yield return ("nose", "sitten");
            yield return ("new", "swear");
            yield return ("wilderness", "discriminate");
            yield return ("swear", "wheel");
            yield return ("point", "pointing");
            yield return ("part", "partnership");
            yield return ("misissipi", "mississippi");
            yield return ("kitten", "sitten");
            yield return ("five", "five");
            yield return ("infrastructure", "infrastructure");
            yield return ("", "ahh");
            yield return ("infrastructure", "");
        }
    }
}