using System;
using BenchmarkDotNet.Running;
namespace Karen.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("KarenBenchmark");
            BenchmarkRunner.Run<Benchmarks>();
            Console.ReadKey();
        }
    }
}
