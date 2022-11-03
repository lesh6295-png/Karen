using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Engine
{
   public static class EventManager
    {
        static Dictionary<string, Action> events = new();

        public static void AddEvent(string name, Action target)
        {
            events.Add(name, target);
        }
        public static void CallEvent(string name)
        {
            events.GetValueOrDefault(name).Invoke();
        }
    }
}
