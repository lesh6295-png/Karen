using Karen.Types;
using Karen.Engine.Scripting;
using System.Threading.Tasks;
using System.Linq;
using System;
namespace Karen.Engine.Api
{
    public static partial class Api
    {
        public static async Task var(object?[]? par)
        {
            VariableContext local = (VariableContext)(((object[])par.Last())[0]);
            Variable newvar = new Karen.Types.Int32((string)par[1], Convert.ToInt32(par[2]));
            if (par.Length >= 5)
            {
                string target = (string)par[3];
                if (target == "global")
                {
                    ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Add(newvar);
                    return;
                }

            }
            local.Add(newvar);
        }

        public static async Task set(object?[]? par)
        {
            string resultvar = (string)par[0];
            string source = (string)par[1];
        }

        public static async Task math(object?[]? par)
        {
            //TODO: implementation
        }
    }
}