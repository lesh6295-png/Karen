using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public class InvalidKeyStringException : Exception
    {
        public InvalidKeyStringException(string s):base(s)
        {

        }
    }
}
