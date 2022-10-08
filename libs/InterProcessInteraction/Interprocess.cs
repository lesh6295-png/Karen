using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Karen.InterProcess
{
    public static class Interprocess
    {
        public static InterprocessItem GetKey(string key)
        {
            return new InterprocessItem(key);
        }
        public static void SetKey(string key, string value)
        {
            string path = Registry.RegController.GetIPIPath();
            File.Delete(path + "\\" + key);
            File.Delete(path + "\\0" + key);
            File.WriteAllText(path + "\\0" + key, value);
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
        }
    }
}
