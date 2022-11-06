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
            Variable newvar = new Karen.Types.Variable((string)par[0], Convert.ToInt32(par[1]));
            if (par.Length >= 4)
            {
                string target = (string)par[2];
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
                target.value = vsource.value;
            }
            else
            {
                target.value = Convert.ToInt32(source);
            }
        }

        public static async Task math(object?[]? par)
        {
            string resultvar = (string)par[0];
            string source1 = (string)par[2];
            string source2 = (string)par[4];
            string operand = (string)par[3];

            if (((string)par[1]) != "=")
            {
                throw new InvalidApiParamsException("Equals symbol dont found.");
            }

            if (!resultvar.StartsWith("_"))
            {
                throw new InvalidApiParamsException("Target variable name invalid.");
            }

            Variable target = ((VariableContext)(((object[])par.Last())[0])).Get(resultvar.Substring(1), false) ?? ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Get(resultvar.Substring(1), false) ?? ((VariableContext)(((object[])par.Last())[0])).Get(resultvar.Substring(1), false, true);
            Variable a, b;

            if (source1.StartsWith("_"))
            {
                a = ((VariableContext)(((object[])par.Last())[0])).Get(source1.Substring(1), false) ?? ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Get(source1.Substring(1), false);
                if (a == null)
                {
                    throw new InvalidApiParamsException("Source variable name invalid.");
                }
            }
            else
            {
                a = new Variable(Extensions.RandomString(),Convert.ToInt32(source1));
            }

            if (source2.StartsWith("_"))
            {
                b = ((VariableContext)(((object[])par.Last())[0])).Get(source2.Substring(1), false) ?? ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Get(source2.Substring(1), false);
                if (b == null)
                {
                    throw new InvalidApiParamsException("Source variable name invalid.");
                }
            }
            else
            {
                b = new Variable(Extensions.RandomString(), Convert.ToInt32(source1));
            }

            switch (operand)
            {
                case "+":
                    target.value = a.value + b.value;
                    break;
                case "-":
                    target.value = a.value - b.value;
                    break;
                case "*":
                    target.value = a.value * b.value;
                    break;
                case "/":
                    target.value = a.value / b.value;
                    break;
                case "%":
                    target.value = a.value % b.value;
                    break;
            }
        }
    }
}