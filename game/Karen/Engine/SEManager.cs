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
            Random();
        }
        async void Random()
        {
            List<SEP> rnd = new();
            foreach (var q in events)
                if (q.type == ScriptingEvent.Random)
                    rnd.Add(q);
            
            int i = 0;
            while (true)
            {
                await Task.Delay(Config.SEPRandomDelay);
                var q = rnd[i % rnd.Count]; 
                if (i / 10 == rnd.Count && i % rnd.Count == 0)
                    i = 0;
                i++;
                
                double pos = double.Parse(q.otherParams, global::System.Globalization.CultureInfo.InvariantCulture);
                double res = Types.Extensions.r.NextDouble();
                if (res <= pos)
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
        public void Autoload()
        {
            //todo: add check for used gui when autoload
            //если в СЕП стартапом помечено нескольео .мику файлов, которые используют окно и разговаривают с пользователям, то они будут прерывать друг друга
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
            if (EngineStarter.VM==null)
                return;
            if (!EngineStarter.VM.AllowHideWindow)
                return;
            foreach (var q in events)
            {
                if (q.type == ScriptingEvent.Process)
                {
                    if (string.IsNullOrWhiteSpace(q.otherParams) || exeName == q.otherParams)
                    {
                        if (new Random().Next() > 500000000)
                            return;
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
