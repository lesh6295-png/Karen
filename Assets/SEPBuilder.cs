using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MessagePack;
using Karen.Types;
using System.Diagnostics;

namespace Karen.Assets
{
    public static class SEPBuilder
    {
        public static void Process()
        {
            Stopwatch qq = Stopwatch.StartNew();
            string[] items = File.ReadAllLines("Assets/ScriptingEventParams");
            List<SEP> res = new();
            res.Capacity = items.Length;
            foreach(var q in items)
            {
                if (string.IsNullOrWhiteSpace(q))
                {
                    continue;
                }
                res.Add(new SEP(q));
            }
            Console.WriteLine($"{res.Count} SEP object generated.");
            byte[] fin = MessagePackSerializer.Serialize<List<SEP>>(res);
            File.WriteAllBytes("bin\\sep.bin",fin);
            qq.Stop();
            Console.WriteLine($"SEP objects generated in {qq.Elapsed}");
        }
    }
}
