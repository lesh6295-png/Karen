using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public static class DynamicSerializator
    {
        public static string ToString(dynamic value)
        {
            string res = "";

            if(value is int)
            {
                res += "int:" + value;
            }
            else if (value is string)
            {
                res += "string:" + value;
            }

            else
            {
                res += "unknown:" + value.ToString();
            }

            return res;
        }

        public static dynamic FromString(string s)
        {
            try
            {
                dynamic d = 0;
                var input = s.Split(':');
                if (input[0] == "int")
                {
                    d = Convert.ToInt32(input[1]);
                }
                else if (input[0] == "string")
                {
                    d = (input[1]);
                }

                else if (input[0] == "unknown")
                {
                    d = input[0];
                }


                return d;
            }
            catch
            {
                throw new InvalidDynamicString("Invalid string.");
            }
        }
    }
}
