using System;



using Karen.Logger;
using Karen.Engine;
using System.Threading;
namespace Karen
{
    class Program
    {
        static void Main(string[] args)
        {
            //Thread.CurrentThread.Priority = ThreadPriority.Highest;
            AppDomain.CurrentDomain.UnhandledException += Logger.Logger.ExceptionLog;

            Logger.Logger.Write("Enter main method.");
            Network.Network.BuildPorts();
            VirtualMachine vm = new VirtualMachine();
            string guid = vm.AddScriptThread();
            var scr = vm.GetScriptContext(guid);
            scr.LoadScriptFromDrive("testscr.miku");
            scr.ExcecuteAsync();
            while (true)
            {
                Thread.Sleep(750);
            }
        }
    }
}