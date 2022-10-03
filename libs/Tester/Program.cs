using System;
namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Karen.Locale.KeySourceFactory.GenerateTestSource("test.locale");
            var q = new Karen.Locale.KeySource("test.locale");
            Console.WriteLine(q.sourceid);
            Console.ReadKey();
        }
    }
}
