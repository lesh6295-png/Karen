using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karen.Types;
using MessagePack;
namespace Karen.Engine
{
    [Serializable]
    [MessagePackObject(keyAsPropertyName: true)]
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
