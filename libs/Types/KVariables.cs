//variables for scripting

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{

    public abstract class Variable
    {
        public string name;
    }
    public class Int32 : Variable
    {
        public int value;
        const string typename = "int32";
        public Int32(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}
