using System;
namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = Karen.InterProcess.Interprocess.GetKey("testkey");
            Console.WriteLine(q.Value);
            Console.ReadKey();
        }
    }
}
