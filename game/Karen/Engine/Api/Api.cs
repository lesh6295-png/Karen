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
        public static async Task DISABLE_HIDE(object?[]? par)
        {
            ((VirtualMachine)((object[])par.Last())[2]).AllowHideWindow = false;
        }
        public static async Task ENABLE_HIDE(object?[]? par)
        {
            ((VirtualMachine)((object[])par.Last())[2]).AllowHideWindow = true;
        }
        public static async Task newsc(object?[]? par)
        {
            ScriptContext nsc = new ScriptContext((VirtualMachine)(((object[])par.Last())[2]));
            nsc.LoadScriptFromByteArray(BinaryManager.Singelton.Extract(Convert.ToInt32((string)par[0]), Convert.ToInt32((string)par[1])));
            nsc.ExcecuteAsync();
        }
        public static async Task save(object?[]? par)
        {
            StateController.SerialiazeVM();
            StateController.SerialazeManagers();
        }
        public static async Task to(object?[]? par)
        {
            ((ScriptContext)(((object[])par.Last())[3])).ToLabel((string)par[0]);
        }
        public static async Task torand(object?[]? par)
        {
            int ran = Types.Extensions.r.Next(1,par.Length) - 1;
            //Logger.Write($"torand command: ran:{ran} par_length:{par.Length} par_length_f:{par.Length - 1}");
            string endpoint = (string)par[ran];
            ((ScriptContext)(((object[])par.Last())[3])).ToLabel(endpoint);
        }
        public static async Task wait(object?[]? par)
        {
#if TESTING
            if (Config.AUTO_TEST)
                return;
#endif
            int time = Convert.ToInt32(par[0]);
            await Task.Delay(time);
        }

        public static async Task quit(object?[]? par)
        {
            int code = par.TryExtractElement<object, int>(0, 0);
            Logger.Write($"QUIT: code: {code}");
            Environment.Exit(code);
        }


        public static async Task _if(object?[]? par)
        {
            string left = (string)par[0];
            string right = (string)par[2];
            string operand = (string)par[1];
            string elsepoint = (string)par[4];
            string endpoint = (string)par[6];
            if ((string)par[3] != "else")
            {
                throw new InvalidApiParamsException("Else block dont found.");
            }
            if ((string)par[5] != "to")
            {
                throw new InvalidApiParamsException("To block dont found.");
            }
            bool istrue;
            Variable a, b;
            a = ((VariableContext)(((object[])par.Last())[0])).Get(left, false) ?? ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Get(left, false);
            if (a == null)
            {
                a = new Variable(Extensions.RandomString(), Convert.ToInt32(left));
            }
            b = ((VariableContext)(((object[])par.Last())[0])).Get(right, false) ?? ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Get(right, false);
            if (b == null)
            {
                b = new Variable(Extensions.RandomString(), Convert.ToInt32(right));
            }
            switch (operand)
            {
                case ">":
                    istrue = a.Value > b.Value;
                    break;
                case "<":
                    istrue = a.Value < b.Value;
                    break;
                case ">=":
                    istrue = a.Value >= b.Value;
                    break;
                case "<=":
                    istrue = a.Value <= b.Value;
                    break;
                case "==":
                    istrue = a.Value == b.Value;
                    break;
                case "!=":
                    istrue = a.Value != b.Value;
                    break;
                default:
                    throw new InvalidApiParamsException("Unknown if comparer.");

            }
            if (!istrue)
            {
                ((ScriptContext)(((object[])par.Last())[3])).ToLabel(elsepoint);
            }
            else
            {
                ((ScriptContext)(((object[])par.Last())[3])).SetIfSkip(elsepoint, endpoint);
            }
        }
    }
}