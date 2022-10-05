using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.NetworkInformation;

namespace Karen.Network
{
    internal static class Port
    {
        static IPGlobalProperties prop;
        static List<int> karenPorts = new List<int>();
        public static int? GetAvaliblePort(int min=1024,int max = UInt16.MaxValue)
        {
            if (max > UInt16.MaxValue)
                throw new ArgumentOutOfRangeException("Too big port value.");

            prop = IPGlobalProperties.GetIPGlobalProperties();
            var usedPorts = Enumerable.Empty<int>()
                .Concat(prop.GetActiveTcpConnections().Select(x => x.LocalEndPoint.Port))
                .Concat(prop.GetActiveTcpListeners().Select(x => x.Port))
                .Concat(prop.GetActiveUdpListeners().Select(x => x.Port))
                .Concat(karenPorts);

            for (; min <= max; min++)
            {
                if (usedPorts.Contains(min))
                {
                    continue;
                }
                karenPorts.Add(min);
                return min;
            }
            
            return null;
        } 
    }
}
