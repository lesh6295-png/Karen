using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.CO
{
    public class NameShorter
    {
        static readonly char[] s36 = "0123456789abcdefghijklmnopqrstuvwxyz".ToCharArray();
        static char get_36_sys_symb(int i)
        {
            return s36[i];
        }
        public static string get36sys(int value)
        {
            int nc = Convert.ToInt32(Math.Round(Math.Log(value + 1, 36), MidpointRounding.ToPositiveInfinity));
            int nd = value, i = 0;
            char[] r = new char[nc];
            while (true)
            {
                int d = nd % 36;
                r[nc - i - 1] = get_36_sys_symb(d);
                nd = (nd - d) / 36;
                if (i + 1 >= nc)
                    break;
                i++;
            }
            StringBuilder b = new();
            b.Append(r);
            return b.ToString();
        }
    }
}
