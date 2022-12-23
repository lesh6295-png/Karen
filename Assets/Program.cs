//this need only if you want build KBL and locales by himself
using System;
using System.IO;
namespace Karen.Assets
{
    class Program
    {
        static void Main(string[] args)
        {
            string netver = $"{Environment.Version.Major}.{Environment.Version.Minor}";
            Console.WriteLine("Net version: " + netver);
            if (args.Length >= 1)
            {
                Console.WriteLine(args[0]);
                string b = $"\\bin\\{args[0]}\\net{netver}-windows\\";
                Environment.CurrentDirectory += b;
            }
            Directory.CreateDirectory("bin\\kbl");
            Directory.CreateDirectory("bin\\locales");
            Console.WriteLine(Environment.CurrentDirectory);
            //build kbls
            string[] libs = File.ReadAllLines("liblist.txt");
            foreach(string s in libs)
            {
                Console.WriteLine(s);
                new Karen.KBL.LibaryBuilder(s.Split(" "));
            }
            //build locales
            ExcelLocale q = new("locales.xlsx");
            q.ParceLocales();
            //build SEP
            SEPBuilder.Process();
            //copy bin dir
            Karen.Types.Extensions.CopyFilesRecursively(new DirectoryInfo("bin"), new DirectoryInfo($"../../../../bin/Karen/{args[0]}/net{netver}-windows"));

            Console.ReadLine();
        }
    }
}
