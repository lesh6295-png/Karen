using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Karen.Engine.System
{
    public class ProcessStartEvent
    {
        public event Action<string> NewProcess;
        public static ProcessStartEvent Singelton;
        public ProcessStartEvent()
        {
            Singelton = this;
            EventManager.Singelton.TryAddEvent("ProcessStart");
            Wait();
        }

        //more check and maybe rewrite this
        async void Wait()
        {
            Process[] lastp, nowp;
            lastp = Process.GetProcesses();
            while (true)
            {
                await Task.Delay(Config.ProcessUpdateDelay);
                nowp = Process.GetProcesses();
                
                if (lastp.Equals(nowp))
                {
                    continue;
                }

                List<Process> newp = new();

                //for(int i = 0, j =0;i<Math.Max(lastp.Length, nowp.Length); i++)
                //{
                //    Process old, new_;
                //    old = lastp[i];
                //    new_ = nowp[i];
                //    if (old.Id!=new_.Id)
                //    {
                //        newp[j] = new_;
                //        j++;
                //    }
                //}
                //REWRITE THIS SHIT
                foreach(var q in nowp)
                {
                    if (!pccnt(lastp, q))
                    {
                        newp.Add(q);
                    }
                }

                foreach(var q in newp)
                {
                    NewProcess?.Invoke(q.ProcessName);
                    EventManager.Singelton.CallEvent("ProcessStart");
                }
                lastp = nowp;
            }
        }
        bool pccnt(Process[] a, Process b)
        {
            foreach (var c in a)
                if (c.Id == b.Id)
                    return true;
            return false;
        }
    }
}
