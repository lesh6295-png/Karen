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
        public async Task WaitUpdate()
        {
            await WaitUpdate(50);
        }
        public async Task WaitUpdate(int checkTimeout)
        {
            while (!File.Exists(path))
            {
                await Task.Delay(checkTimeout);
            }
        }
        string cached = null;
        string key = "";
        string path
        {
            get
            {
                return Registry.RegController.GetIPIPath() + "0" + key;
            }
        }
        string? get()
        {
            if (File.Exists(path))
            {
                CacheToMemory();
            }
            return cached;
        }
        internal void CacheToMemory()
        {
            try
            {
                cached = File.ReadAllText(path);
                new FileInfo(path).MarkReaded();
            }
            catch 
            {
            
            }
        }
        internal InterprocessItem(string key)
        {
            this.key = key;
        }
    }
}
