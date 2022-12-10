using System.IO;
using System;
using System.Threading.Tasks;
using Karen.Types;
using System.Text;
using System.Text.Json;
using Karen.Engine.Scripting;
using Karen.Engine;
using System.Linq;
using System.Diagnostics;
namespace Karen.Engine.Api
{
    public  static partial class Api
    {
        public static async Task dump(object?[]? par)
        {
                    string dump = JsonSerializer.Serialize(((object[])par.Last())[2], typeof(VirtualMachine), new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
                    File.WriteAllText("vm_dump.json", dump);
        }
        public static async Task log(object?[]? message)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var q in message)
            {
                if (q != null)
                    if (q is string w)
                        sb.Append(w + " ");
            }
            Logger.Write(sb.ToString());
        }

        public static async Task ram(object?[]? par)
        {
            var th = Process.GetCurrentProcess().Threads;
            var process = Process.GetCurrentProcess();
            StringBuilder bld = new();
            bld.Append($"vmem64:{process.VirtualMemorySize64};");
            bld.Append($"pmem64:{process.PrivateMemorySize64};");
            bld.Append($"treads:{th.Count};");
            Logger.Write(bld.ToString());
        }
    }
}