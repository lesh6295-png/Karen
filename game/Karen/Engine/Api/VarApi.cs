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
            Variable newvar = new Karen.Types.Variable((string)par[1], Convert.ToInt32(par[2]));
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

            if (!resultvar.StartsWith("_"))
            {
                throw new InvalidApiParamsException("Target variable name invalid.");
            }

            Variable target = ((VariableContext)(((object[])par.Last())[0])).Get(resultvar.Substring(1), false) ?? ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Get(resultvar.Substring(1), false) ?? ((VariableContext)(((object[])par.Last())[0])).Get(resultvar.Substring(1), false, true);

            if (source.StartsWith("_"))
            {
                Variable vsource = ((VariableContext)(((object[])par.Last())[0])).Get(source.Substring(1), false) ?? ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Get(source.Substring(1), false);
                if (vsource == null)
                {
                    throw new InvalidApiParamsException("Source variable name invalid.");
                }
                target.value += vsource.value;
            }
            else
            {
                target.value += Convert.ToInt32(source);
            }
        }

        public static async Task math(object?[]? par)
        {
            //TODO: implementation
        }
    }
}