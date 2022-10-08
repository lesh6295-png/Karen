using System;
namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Karen.InterProcess.Interprocess.ClearAllKeys();
            Console.ReadKey();
        }
    }
}
