using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Karen.Logger;
namespace Karen.Network
{
    static class Network
    {
        public static int UIInputPort { get; private set; }
        public static int UICallbackPort { get; private set; }

        internal static void BuildPorts()
        {
            UIInputPort = Port.GetAvaliblePort()??48975;
            Logger.Logger.Write($"UIInputPort: {UIInputPort}");
            UICallbackPort = Port.GetAvaliblePort() ?? 26543;
            Logger.Logger.Write($"UICallbackPort: {UICallbackPort}");
        } 
    }
}
