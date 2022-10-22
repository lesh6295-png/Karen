using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using Karen.Network;
using System.Net.Http;
using Karen.Types;
using System.IO.Pipes;
namespace Karen.Engine
{
    public static class UIWindow
    {
        static string path = "bin\\UI\\net5.0-windows\\KarenRender.exe";
        static Process render;
        static NamedPipeClientStream client;

        static StreamReader reader;
        static StreamWriter writer;
        public static void CreateProcess()
        {
            client = new NamedPipeClientStream("nerakuipipe");
            reader = new StreamReader(client);
            writer = new(client);
            render = new Process();
            render.StartInfo.FileName = path;
            render.StartInfo.Arguments = $"-port {Network.Network.UIInputPort} -callback {Network.Network.UICallbackPort}";
            render.Start();
        }

        public static async Task Say(string message, bool wait = false)
        {
            if (render == null)
                throw new ProcessNotRunningException("KarenRender process not started.");
            if (!client.IsConnected)
            {
                await client.ConnectAsync(4*5000);
                if (!client.IsConnected)
                    throw new ProcessNotRunningException("KarenRender process not started.");
            }

            IPCData command = new IPCData();
            command.Type = "write";
            command.FirstParam = message;
            command.SecondParam = wait;
            string ser = command.ToString();
            writer.WriteLine(ser);
            string responce = reader.ReadLine();



            if (wait)
            {
                //q.Wait();
            }
        }
    }
}
