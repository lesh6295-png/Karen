using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Karen.Network;
using System.Net.Http;
using System.Net;
namespace Karen.Engine
{
    public static class UIWindow
    {
        static string path = "bin\\UI\\net5.0-windows\\KarenRender.exe";
        static Process render;
        static HttpClient client;
        static HttpListener reciver;
        public static bool IsReady { get; private set; } = false;
        public static void CreateProcess()
        {
            client = new HttpClient();
            reciver = new HttpListener();
            reciver.Prefixes.Add($"http://localhost:{Network.Network.UICallbackPort}/");
            reciver.Start();

            Task.Run(ProcessConnections);
            render = new Process();
            render.StartInfo.FileName = path;
            render.StartInfo.Arguments = $"-port {Network.Network.UIInputPort} -callback {Network.Network.UICallbackPort}";
            render.Start();
        }
        static async Task ProcessConnections()
        {
            while (true)
            {
                var q = await reciver.GetContextAsync();
                string type = q.Request.QueryString["type"];
                string responce = "0";
                switch (type)
                {
                    case "ready":
                        IsReady = true;
                        responce = "1";
                        break;
                }
                byte[] arr = Encoding.UTF8.GetBytes(responce);
                q.Response.OutputStream.Write(arr, 0, arr.Length);
                q.Response.ContentLength64 = arr.Length;
                q.Response.Close();
                await Task.Delay(100);
            }
        }

        public static async Task Say(string message, bool wait = false)
        {
            if (render == null || IsReady==false)
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
