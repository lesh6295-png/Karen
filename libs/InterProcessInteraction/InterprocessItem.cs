using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Karen.Registry;
namespace Karen.InterProcess
{
    public class InterprocessItem
    {
        public string? Value
        {
            get
            {
                return get();
            }
        }
        string cached = null;
        public string key { get; private set; }
        public async Task WaitToChange()
        {
            string local = (string)(cached??"").Clone();
            while (local == cached)
            {
                await Task.Delay(60);
            }
        }
        string path
        {
            get
            {
                return Registry.RegController.GetIPIPath() + "\\" + key;
            }
        }
        string? get()
        {
            return cached;
        }
        internal void CacheToMemory()
        {
            try
            {
                cached = File.ReadAllText(path);
            }
            catch 
            {
                //Set null if file deleting
                cached = null;
            }
        }
        internal bool Exists()
        {
            return File.Exists(path);
        }
        internal InterprocessItem(string key)
        {
            this.key = key;
        }
    }
}
