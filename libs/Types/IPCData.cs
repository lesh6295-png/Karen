using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
namespace Karen.Types
{
    /// <summary>
    /// This class used to interprocess interactive.
    /// </summary>
    public class IPCData
    {
        public string Type { get; private set; }
        public object FirstParam { get; private set; }
        public object SecondParam { get; private set; }

        public override string ToString()
        {
            JsonSerializerOptions op = new JsonSerializerOptions();
#if DEBUG
            op.WriteIndented = true;
#endif
            return JsonSerializer.Serialize<IPCData>(this);
        }

        public static IPCData FromString(string source)
        {
            return JsonSerializer.Deserialize<IPCData>(source);
        }
    }
}
