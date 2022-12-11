using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karen.Types;
using MessagePack;
using System.IO;
namespace Karen.Engine
{
    [Serializable]
    [MessagePackObject(keyAsPropertyName: true)]
    public class EventManager : IManagerSerializator
    {
        public static EventManager Singelton;
        static EventManager()
        {
            Singelton = new();
        }
        Dictionary<string, KarenEvent> events = new();
        public void DeleteEvent(string name)
        {
            events.Remove(name);
        }
        public void AddEvent(string name)
        {
            events.Add(name, new KarenEvent());
        }
        public void TryAddEvent(string name)
        {
            if (events.ContainsKey(name)) return;
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

        public void Load()
        {
            byte[] listraw = File.ReadAllBytes(targetfolder + "events");
            var list = MessagePackSerializer.Deserialize<List<string>>(listraw);
            list.ForEach((x) => { TryAddEvent(x); });
        }

        public void Save()
        {
            var list = events.Keys.ToList();
            byte[] arr = MessagePackSerializer.Serialize<List<string>>(list);
            File.WriteAllBytes(targetfolder + "events", arr);
        }

        public void SetFolder(string folder)
        {
            targetfolder = folder;
        }
        string targetfolder = "";
    }
}
