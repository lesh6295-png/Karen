using Karen.Types;
using Karen.Engine.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MessagePack;
namespace Karen.Engine
{
    public class SEManager
    {
        public static SEManager Singelton;
        public SEManager()
        {
            Singelton = this;
            ProcessStartEvent.Singelton.NewProcess += Process;
            events = MessagePackSerializer.Deserialize<List<SEP>>(File.ReadAllBytes("bin\\sep.bin"));
        }

        private void Process(string exeName)
        {
            throw new NotImplementedException();
        }

        List<SEP> events;

    }
}
