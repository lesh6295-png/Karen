//this need only if you want build KBL by himself
using System;
using System.IO;
namespace Karen.Assets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);
            string b = $"\\bin\\{args[0]}\\net5.0\\";
            Environment.CurrentDirectory += b;
            Directory.CreateDirectory("kbl");
            Console.WriteLine(Environment.CurrentDirectory);
            string[] libs = File.ReadAllLines("liblist.txt");
            foreach(string s in libs)
            {
                Console.WriteLine(s);
                new Karen.KBL.LibaryBuilder(s.Split(" "));
            }
            Console.ReadLine();
        }
    }
}
