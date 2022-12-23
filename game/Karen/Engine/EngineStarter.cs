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
using Karen.Engine.System;
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
            new ProcessStartEvent();
            AppDomain.CurrentDomain.UnhandledException += Logger.ExceptionLog;
            if(Config.ChangeDir)
            Environment.CurrentDirectory = Karen.Types.Extensions.GetExePath();
            Logger.Write($"Active working directory: {Environment.CurrentDirectory}");

            if (!StateController.Enable())
            {
                WriteRegistry();
                VM = new VirtualMachine();
                int mainid = BinaryManager.Singelton.LoadKBL("bin\\kbl\\main.kbl");
                byte[] main = BinaryManager.Singelton.Extract(mainid, 1);
                Karen.Types.Guid mainthreadguid = VM.AddScriptThread();
                ScriptContext maincon = VM.GetScriptContext(mainthreadguid);
                maincon.LoadScriptFromByteArray(main);
                maincon.ExcecuteAsync();
            }
            else
            {
                StateController.LoadSave();
                SEManager.Singelton.Autoload();
            }
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
