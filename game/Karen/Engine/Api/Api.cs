using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if(par[0] is string q)
            {
                nsc.LoadScriptFromDrive(q);
                nsc.ExcecuteAsync();
            }
        }
        public static async Task wait(object?[]? par)
        {
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