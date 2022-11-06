using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karen.Types;
namespace Karen.Engine
{
   public static class EventManager
    {
        static Dictionary<string, KarenEvent> events = new();
        
        public static void AddEvent(string name)
        {
            events.Add(name, new KarenEvent());
        }
        public static void CallEvent(string name)
        {
            events.GetValueOrDefault(name).Invoke();
        }
        public static void AddListerner(string name, Action action)
        {
            events.GetValueOrDefault(name).AddListerner(action);
        }
        public static async Task Wait(string name)
        {
            var w = events.GetValueOrDefault(name);
            while (w == null)
            {
                events.GetValueOrDefault(name);
            }
            await w.Wait();
        }
    }
}
