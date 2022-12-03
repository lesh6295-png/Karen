//like a YandereDev
using System;
using System.Linq;
using System.IO;
using Karen.Types;
namespace Karen.InstallerBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
#if TESTING
            //return;
#endif
            string config = args[0];
           // string netver = args[1].Split('/').Last().Replace("\\","");
            Console.WriteLine("Start directory: "+Environment.CurrentDirectory);
            Environment.CurrentDirectory += "/../../../../../../bin";
            //delete last build result
            try
            {
                //if (Directory.Exists("Installer"))
                //    Directory.Delete("Installer", true);
                //Directory.CreateDirectory("Installer");

                if (!Directory.Exists("Installer"))
                //////    Directory.Delete("Installer", true);
                Directory.CreateDirectory("Installer");
            }
            catch
            {

            }
            //copy Karen archives binarys
            string[] karenbins = Directory.GetFiles("Karen/", "game.zip", SearchOption.AllDirectories);
            foreach(var q in karenbins)
            {
                string shortname = "";
                if (q.Contains("Debug"))
                {
                    shortname += "d";
                }
                else if (q.Contains("Testing"))
                {
                    shortname += "t";
                }
                else if (q.Contains("Release"))
                {
                    shortname += "r";
                }

                if (q.Contains("5"))
                {
                    shortname += "5";
                }
                else if (q.Contains("6"))
                {
                    shortname += "6";
                }
                else if (q.Contains("7"))
                {
                    shortname += "7";
                }

                shortname += ".bin";
                File.Move(q, $"Installer/{shortname}", true);
            }

            //copy installer.exe
            try
            {
                File.Copy($"installer-core/{config}/installer.exe", "Installer/installer.exe", true);
            }
            catch { }

            int CompareGuiBins(string x, string y)
            {
                int r = 0;
                if (x.Contains("Testing") && (y.Contains("Debug") || y.Contains("Release"))) r = 1;
                if (x.Contains("Debug") && y.Contains("Release")) r = 1;
                if (y.Contains("Testing") && (x.Contains("Debug") || x.Contains("Release"))) r = -1;
                if (y.Contains("Debug") && x.Contains("Release")) r = -1;

                if (x.Contains("5") && (y.Contains("6") || y.Contains("7"))) r = 1;
                if (x.Contains("6") && y.Contains("7")) r = 1;
                if (y.Contains("5") && (x.Contains("6") || x.Contains("7"))) r = -1;
                if (y.Contains("6") && x.Contains("7")) r = -1;
                return r;
            }
            //copy gui installer
            string[] guis = Directory.GetFiles("InstallerTemp/", "gui.zip", SearchOption.AllDirectories);
            guis.ToList().Sort(CompareGuiBins);
            File.Copy(guis[0], "Installer/gui.zip", true);
        }
    }
}
