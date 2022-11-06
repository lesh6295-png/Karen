using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karen.KBL;
using Karen.Locale;
using Karen.Engine.Scripting;
using Karen.Types;
using System.IO;
namespace Karen.Engine.Api
{
    public static partial class Api
    {
        public static async Task newsc(object?[]? par)
        {
            ScriptContext nsc = new ScriptContext((VirtualMachine)(((object[])par.Last())[2]));
            nsc.LoadScriptFromByteArray(BinaryManager.Extract(Convert.ToInt32((string)par[0]), Convert.ToInt32((string)par[1])));
                nsc.ExcecuteAsync();
            }
        public static async Task to(object?[]? par)
        {
            ((ScriptContext)(((object[])par.Last())[3])).ToLabel((string)par[0]);
        }
        public static async Task wait(object?[]? par)
        {
#if TESTING
            if (App.AUTO_TEST)
                return;
#endif
            int time = Convert.ToInt32(par[0]);
            await Task.Delay(time);
        }

        public static async Task quit(object?[]? par)
        {
            int code = par.TryExtractElement<object,int>(0, 0);
            Logger.Write($"QUIT: code: {code}");
            Environment.Exit(code);
        }
        
    }
}