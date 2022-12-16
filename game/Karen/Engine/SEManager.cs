using Karen.Types;
using Karen.Engine.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karen.KBL;
using System.IO;
using Karen.Engine.Scripting;
using System.Threading.Tasks;
using MessagePack;
namespace Karen.Engine
{
    public class SEManager
    {
        public static SEManager Singelton;
        VirtualMachine m;
        public SEManager(VirtualMachine m)
        {
            this.m = m;
            Singelton = this;
            ProcessStartEvent.Singelton.NewProcess += Process;
            events = MessagePackSerializer.Deserialize<List<SEP>>(File.ReadAllBytes("bin\\sep.bin"));
        }
        void Autoload()
        {
            foreach(var q in events)
            {
                if (q.type == ScriptingEvent.Startup)
                {
                    byte[] rawcode;
                    try
                    {
                        rawcode = BinaryManager.Singelton.Extract(q.kblId, q.kblPos);
                    }
                    catch (Karen.Types.ObjectNotFoundException e)
                    {
                        BinaryManager.Singelton.LoadKBL(q.kblPath);
                        rawcode = BinaryManager.Singelton.Extract(q.kblId, q.kblPos);
                    }

                    ScriptContext c = m.GetScriptContext(m.AddScriptThread());
                    c.LoadScriptFromByteArray(rawcode);
                    c.ExcecuteAsync();
                }
            }
        }
        private void Process(string exeName)
        {
            if (!EngineStarter.VM.AllowHideWindow)
                return;
            foreach (var q in events)
            {
                if (q.type == ScriptingEvent.Process)
                {
                    if (new Random().Next() > 500000000)
                        return;
                    if (string.IsNullOrWhiteSpace(q.otherParams) || exeName == q.otherParams)
                    {
                        byte[] rawcode;
                        try
                        {
                            rawcode = BinaryManager.Singelton.Extract(q.kblId, q.kblPos);
                        }
                        catch (Karen.Types.ObjectNotFoundException e)
                        {
                            BinaryManager.Singelton.LoadKBL(q.kblPath);
                            rawcode = BinaryManager.Singelton.Extract(q.kblId, q.kblPos);
                        }

                        ScriptContext c = m.GetScriptContext(m.AddScriptThread());
                        c.LoadScriptFromByteArray(rawcode);
                        c.ExcecuteAsync();
                    }
                }
            }
        }

        List<SEP> events;

    }
}
