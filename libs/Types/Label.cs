using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
namespace Karen.Types
{

    [Serializable]
    [MessagePackObject(keyAsPropertyName: true)]
    public struct Label
    {
        public string name;
        public int position;
    }
}
