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
            UIWindow.CreateProcess();
            UIWindow.Say("Hi!").Wait();
            VirtualMachine vm = new VirtualMachine();
            string guid = vm.AddScriptThread();
            var scr = vm.GetScriptContext(guid);
            var ml = new Karen.KBL.BinaryLibary("bin\\kbl\\main.kbl");
            scr.LoadScriptFromByteArray(ml.Extract(1));
            scr.ExcecuteAsync();
            while (true)
            {
                Thread.Sleep(750);
            }
        }
    }
}