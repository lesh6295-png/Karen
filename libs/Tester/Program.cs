using System;
namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "write")
            {
                while (true)
                {
                    Karen.InterProcess.Interprocess.SetKey("test", Karen.Types.Extensions.RandomString());
                    System.Threading.Thread.Sleep(500);
                }
            }
            if (args[0] == "read")
                
            {
                var q = Karen.InterProcess.Interprocess.GetKey("test");
                while (true)
                {
                    Console.WriteLine(q.Value);
                    System.Threading.Thread.Sleep(500);
                }
            }
            Console.ReadKey();
        }
    }
}
