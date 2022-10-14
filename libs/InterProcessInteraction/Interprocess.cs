using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Karen.Types;
namespace Karen.InterProcess
{
    public static class Interprocess
    {
        static FileSystemWatcher kuh;
        static List<InterprocessItem> items = new List<InterprocessItem>(); 
        public static InterprocessItem GetKey(string key)
        {
            InterprocessItem res = items.GetElement((x) => x.key.Equals(key));
            if (res == null)
            {
                res = new InterprocessItem(key);
                if (res.Exists())
                {
                    res.CacheToMemory();
                    items.Add(res);
                }
                else
                {
                    throw new Karen.Types.ObjectNotFoundException($"Interprocess item with key {key} don`t exsist.");
                }
            }
            return res;
        }
        public static void SetKey(string key, string value)
        {
            string path = Registry.RegController.GetIPIPath();
            File.Delete(path + "\\" + key);
            File.WriteAllText(path + "\\" + key, value);
        }
        /// <summary>
        /// This method will be delete all exsicting interprocess keys.
        /// Your (maybe) dont need this
        /// </summary>
        public static void ClearAllKeys()
        {
            DirectoryInfo q = new DirectoryInfo(Registry.RegController.GetIPIPath());
            var e = q.GetFiles();
            foreach(var w in e)
            {
                File.Delete(w.FullName);
            }
        }
        static Interprocess()
        {
            Directory.CreateDirectory(Registry.RegController.GetIPIPath());
            kuh = new FileSystemWatcher(Karen.Registry.RegController.GetIPIPath());
            kuh.EnableRaisingEvents = true;
            kuh.Changed += UpdateKey;
            kuh.InternalBufferSize = 1024 * 64;
        }


        private static void UpdateKey(object sender, FileSystemEventArgs e)
        {
            //i take this condition from MDSN
            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            InterprocessItem change = items.GetElement((x) => x.key == e.Name);
            if (change != null)
                change.CacheToMemory();
        }
    }
}
