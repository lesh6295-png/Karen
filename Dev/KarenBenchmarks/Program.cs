using System;
using BenchmarkDotNet.Running;
namespace Karen.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("KarenBenchmark");
            //new Benchmarks().PipesConnectToServer();
            BenchmarkRunner.Run<Benchmarks>();
            Console.ReadKey();
        }
    }
}
