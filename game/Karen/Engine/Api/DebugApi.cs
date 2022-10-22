using System.IO;
using System;
using System.Threading.Tasks;
using Karen.Types;
using System.Text;
using Karen.Engine.Scripting;
using Karen.Engine;
using System.Linq;

namespace Karen.Engine.Api
{
    internal  static partial class Api
    {
        public static async Task debug(object?[]? par)
        {
            string mode = par.TryExtractElement<object, string>("unk");

            switch (mode)
            {
                case "vm_dump":
                    string dump = System.Text.Json.JsonSerializer.Serialize(((object[])par.Last()).Last(), typeof(VirtualMachine), new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
                    File.WriteAllText("vm_dump.json", dump);
                    break;
            }
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
    }
}