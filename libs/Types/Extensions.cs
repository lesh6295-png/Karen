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
        static Random r = new Random();
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
        public static DirectoryInfo CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            var newDirectoryInfo = target.CreateSubdirectory(source.Name);
            foreach (var fileInfo in source.GetFiles())
                fileInfo.CopyTo(Path.Combine(newDirectoryInfo.FullName, fileInfo.Name), true);

            foreach (var childDirectoryInfo in source.GetDirectories())
                CopyFilesRecursively(childDirectoryInfo, newDirectoryInfo);

            return newDirectoryInfo;
        }

        /// <summary>
        /// Rename file to newName
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <param name="newName">New name to file</param>
        public static void Rename(this FileInfo fileInfo, string newName)
        {
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
        }

        public static T GetElement<T>(this List<T> list, Predicate<T> condition)
        {
            foreach(var q in list)
            {
                if (condition(q))
                    return q;
            }
            return default(T);
        }
    }
}
