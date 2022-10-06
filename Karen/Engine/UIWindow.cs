using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Karen.Network;
using System.Net.Http;
namespace Karen.Engine
{
    public static class UIWindow
    {
        static string path = "bin\\UI\\net5.0-windows\\KarenRender.exe";
        static Process render;
        static HttpClient client;
        public static void CreateProcess()
        {
            client = new HttpClient();
            render = new Process();
            render.StartInfo.FileName = path;
            render.StartInfo.Arguments = $"-port {Network.Network.UIInputPort} -callback {Network.Network.UICallbackPort}";
            render.Start();
        }

        public static async Task Say(string message, bool wait = false)
        {
            if (render == null)
                return;
            string url = $"http://localhost:{Network.Network.UIInputPort}/?type=write&text={message}&wait={Convert.ToString(wait)}";
            var q = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, url));
            if (wait)
            {
                q.Wait();
            }
        }
    }
}
