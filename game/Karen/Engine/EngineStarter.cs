using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karen.Registry;
using System.Threading.Tasks;
using System.Diagnostics;
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
            //allow only one instance
            if (OneInstance())
            {
                Environment.Exit(-2);
            }

            AppDomain.CurrentDomain.UnhandledException += Logger.ExceptionLog;
            throw new System.Exception();
            WriteRegistry();
            VM = new VirtualMachine();
            int mainid = BinaryManager.LoadKBL("bin\\kbl\\main.kbl");
            byte[] main = BinaryManager.Extract(mainid,1);
            string mainthreadguid = VM.AddScriptThread();
            ScriptContext maincon = VM.GetScriptContext(mainthreadguid);
            maincon.LoadScriptFromByteArray(main);
            maincon.ExcecuteAsync();
            while (true)
            {
                Thread.Sleep(750);
            }
        }


        static void WriteRegistry()
        {
            if (!RegController.IsInstalled())
            {
                RegController.SetKarenFolderPath($"C:\\ProgramData\\neraK\\");
                RegController.WriteState(Types.ClientState.Installed);
            }
        }
        static bool OneInstance()
        {
            return Process.GetProcessesByName("Karen").Length > 1;
        }
    }
}
