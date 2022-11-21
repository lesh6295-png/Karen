//variables for scripting

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using MessagePack;
namespace Karen.Types
{

    [Serializable]
    [MessagePackObject(keyAsPropertyName: true)]
    public class Variable
    {
        public string name;
        [IgnoreMember]
        [JsonIgnore]
         dynamic value;
        [JsonIgnore]
        [IgnoreMember]
        public dynamic Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                svalue = DynamicSerializator.ToString(value);
            }
        }
        /// <summary>
        /// Dont touch this filed.
        /// </summary>
        public string svalue;
        public Variable(string name, dynamic value)
        {
            this.name = name;
            this.value = value;
        }

        public Variable()
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
