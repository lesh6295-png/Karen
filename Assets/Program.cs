//this need only if you want build KBL and locales by himself
using System;
using System.IO;
namespace Karen.Assets
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                Console.WriteLine(args[0]);
                string b = $"\\bin\\{args[0]}\\net5.0\\";
                Environment.CurrentDirectory += b;
            }
            Directory.CreateDirectory("bin\\kbl");
            Directory.CreateDirectory("bin\\locales");
            Console.WriteLine(Environment.CurrentDirectory);
            string[] libs = File.ReadAllLines("liblist.txt");
            foreach(string s in libs)
            {
                Console.WriteLine(s);
                new Karen.KBL.LibaryBuilder(s.Split(" "));
            }

            ExcelLocale q = new("locales.xlsx");
            q.ParceLocales();
            Karen.Types.Extensions.CopyFilesRecursively(new DirectoryInfo("bin"), new DirectoryInfo($"../../../../bin/{args[0]}/net5.0-windows"));
            Console.ReadLine();
        }
    }
}
