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
