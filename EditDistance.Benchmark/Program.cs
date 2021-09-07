using BenchmarkDotNet.Running;
using System;

namespace EditDistance.Benchmark {
    class Program {
        static void Main(string[] args) {
            BenchmarkRunner.Run<EditDistanceTest>();
        }
    }
}
