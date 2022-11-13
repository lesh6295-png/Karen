using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karen.Types;
namespace Karen.Engine
{
    [Serializable]
    public class EventManager
    {
        Dictionary<string, KarenEvent> events = new();
        
        public void AddEvent(string name)
        {
            events.Add(name, new KarenEvent());
        }
        public void CallEvent(string name)
        {
            events.GetValueOrDefault(name).Invoke();
        }
        public void AddListerner(string name, Action action)
        {
            events.GetValueOrDefault(name).AddListerner(action);
        }
        public async Task Wait(string name)
        {
            await events.GetValueOrDefault(name)?.Wait();
        }
    }
}
