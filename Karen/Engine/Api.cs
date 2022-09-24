using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Engine
{
    internal static class Api
    {
        public static async Task log(object?[]? message)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var q in message)
            {
                if (q != null)
                    if (q is string w)
                        sb.Append(w + " ");
            }
            Logger.Logger.Write(sb.ToString());
        }
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
            int time = Convert.ToInt32((string)par[0]);
            await Task.Delay(time);
        }
        public static async Task var(object?[]? par)
        {
            VariableContext local = (VariableContext)(((object[])par.Last())[0]);
            var vars = Karen.Types.TypesFactory.BuildVariable((string)par[0], (string)par[1], par[2]);
            if (par.Length >= 5)
            {
                string target = (string)par[3];
                if(target == "global")
                {
                    ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Add(vars);
                    return;
                }
                
            }
            local.Add(vars);
        }
    }
}