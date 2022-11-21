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

    public class InvalidApiParamsException : Exception
    {
        public InvalidApiParamsException(string message) : base(message)
        {

        }
    }

    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string m) : base(m)
        {

        }
    }
    public class ProcessNotRunningException : Exception
    {
        public ProcessNotRunningException(string m) : base(m)
        {

        }
    }

    public class DublicateObjectException : Exception
    {
        public DublicateObjectException(string m) : base(m)
        {

        }
    }

    public class InvalidDynamicString : Exception
    {
        public InvalidDynamicString(string m) : base(m)
        {

        }
    }
}
