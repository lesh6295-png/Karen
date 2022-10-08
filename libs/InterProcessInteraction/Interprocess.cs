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
        static Interprocess()
        {
            Directory.CreateDirectory(Registry.RegController.GetIPIPath());
        }
    }
}
