﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Karen.Engine.Scripting;
using Karen.KBL;
namespace Karen.Engine
{
    public static class EngineStarter
    {
        public static VirtualMachine VM;
        public static void Start()
        {
            AppDomain.CurrentDomain.UnhandledException += Logger.ExceptionLog;
            VM = new VirtualMachine();
            BinaryLibary mainLib = new BinaryLibary("bin\\kbl\\main.kbl");
            byte[] main = mainLib.Extract(1);
            string mainthreadguid = VM.AddScriptThread();
            ScriptContext maincon = VM.GetScriptContext(mainthreadguid);
            maincon.LoadScriptFromByteArray(main);
            maincon.ExcecuteAsync();
            while (true)
            {
                Thread.Sleep(750);
            }
        }
    }
}