﻿using System;
using System.Linq;
using System.IO;
using Karen.Types;
namespace Karen.InstallerBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string config = args[0];
            string netver = args[1].Split('/').Last().Replace("\\","");
            Console.WriteLine("Start directory: "+Environment.CurrentDirectory);
            Environment.CurrentDirectory += "/../../../../../../bin";
            //delete last build result
            try
            {
                if (Directory.Exists("Installer"))
                    Directory.Delete("Installer", true);
            }
            catch
            {

            }
            string path = $"Installer/{config}/{netver}";
            Directory.CreateDirectory(path+"/offline");
            Directory.CreateDirectory(path + "/local");
            Directory.CreateDirectory(path + "/minimal");

            //copy installer.exe
            File.Copy("installer-core/" + config + "/installer.exe", path + "/offline/installer.exe");
            File.Copy("installer-core/" + config + "/installer.exe", path + "/local/installer.exe");
            File.Copy("installer-core/" + config + "/installer.exe", path + "/minimal/installer.exe");

            //copy 1.bin
            File.Copy("InstallerTemp/" + config + "/"+netver+"/installer.zip", path + "/offline/1.bin");
            File.Copy("InstallerTemp/" + config + "/" + netver + "/installer.zip", path + "/local/1.bin");

            //todo: add copy raw karen to offline build


            //write config
            string globalr = Karen.Types.Extensions.RandomString(min: 10, max: 20).ToHex();
            File.WriteAllText(path + "/config.ini", globalr);
            File.WriteAllText(path + "/offline/config.ini", globalr);
            File.WriteAllText(path + "/local/config.ini", globalr);
            File.WriteAllText(path + "/minimal/config.ini", globalr);
        }
    }
}
