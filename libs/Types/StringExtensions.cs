using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public static class StringExtensions
    {
        static Random r = new Random();
        public static string RandomString(int min=5, int max = 256)
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
    }
}
