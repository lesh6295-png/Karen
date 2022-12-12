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
    public class SEP
    {
        public ScriptingEvent type;
        public string kblPath;
        public int kblPos, kblId;
        public string otherParams;
        public SEP()
        {

        }
        public SEP(string rawSource)
        {
            string[] rawparams = rawSource.Split(' ');
            //из-за того, что rawSource делится по прообелам, в kblPath и otherParams для правильной работы все пробелы надо заменить на !space! 
            kblPath = rawparams[0].Replace("!space!"," ");
            type = (ScriptingEvent)Enum.Parse(typeof(ScriptingEvent), rawparams[3]);
            kblId = Convert.ToInt32(rawparams[1]);
            kblPos = Convert.ToInt32(rawparams[2]);
            otherParams = rawparams[4].Replace("!space!", " ");
        }
    }
    public enum ScriptingEvent : byte
    {
        Process=0,
        Random=1
    }
}
