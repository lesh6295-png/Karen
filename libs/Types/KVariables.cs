//variables for scripting

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace Karen.Types
{

    [Serializable]
    public class Variable : ISerializable
    {
        public string name;
        public dynamic value;
        public Variable(string name, dynamic value)
        {
            this.name = name;
            this.value = value;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", this.name);
        }
        public Variable()
        {

        }
        public Variable(SerializationInfo info, StreamingContext context)
        {

        }
    }
    /*public class Int32 : Variable
    {
        public int value;
        const string typename = "int32";
        public Int32(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }*/
}
