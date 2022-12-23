using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Karen.Types
{
    public static class Extensions
    {
        public static Random r = new Random();
        public static string RandomString(int min=5, int max = 14)
        {
            string lib = "0987654321qwertyuioplkjhgfdsazxcvbnmQWERTYUIOPLKJHGFDAZXCVBNM";
            StringBuilder bld = new StringBuilder();
            int size = r.Next(min, max + 1);
            for(int i = 0; i < size; i++)
            {
                bld.Append(lib[r.Next(lib.Length)]);
            }
            return bld.ToString();
        }

        public static R TryExtractElement<T, R>(this object Arr, R defaultValue,  int index = 0)
        {
            var arr = (object[])Arr;
            if (arr.Length < index)
                return defaultValue;
            object q = arr[index];
            if (q is R r)
                return r;
            return defaultValue;
        }

        public static string GetExePath()
        {
            string res = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            if (res.StartsWith("file:\\"))
            {
                res = res.Substring(6);
            }
            return res;
        }

        public static DirectoryInfo CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            var newDirectoryInfo = target.CreateSubdirectory(source.Name);
            foreach (var fileInfo in source.GetFiles())
                fileInfo.CopyTo(Path.Combine(newDirectoryInfo.FullName, fileInfo.Name), true);

            foreach (var childDirectoryInfo in source.GetDirectories())
                CopyFilesRecursively(childDirectoryInfo, newDirectoryInfo);

            return newDirectoryInfo;
        }

        public static string ToHex(this string s)
        {
            var sb = new StringBuilder();

            var q = Encoding.Unicode.GetBytes(s);
            for (int i = 0; i < q.Length; i++)
            {
                sb.Append(q[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string FromHex(this string s)
        {
            var q = new byte[s.Length / 2];
            for (var i = 0; i < q.Length; i++)
            {
                q[i] = Convert.ToByte(s.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(q);
        }
    }
}
