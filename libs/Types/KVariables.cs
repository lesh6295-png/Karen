//variables for scripting

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public enum VariableType
    {
        Int16=0,
        Int32=1,
        Int64=2,
        Uint16=4,
        Uint32=8,
        Uint64=16,
        Boolean=32,
        String=64,
        Double=128,
        Decimal=256
    }

    public struct kvar
    {
        public VariableType Type { get; private set; }
        public string name;
        public object value;
        public kvar(VariableType type, string name, object value)
        {
            Type = type;
            this.name = name;
            this.value = value;
        }
    }
}
