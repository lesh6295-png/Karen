using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Karen.Network;
using System.Net.Http;
using Karen.InterProcess;
using System.Net;
namespace Karen.Engine
{
    public static class UIWindow
    {
        static string path = "bin\\UI\\net5.0-windows\\KarenRender.exe";
        static Process render;
        static Task readyCallback;
        static InterprocessItem uistart, uicallback, text_path, type, body_wait;
        public static bool IsReady { get; private set; } = false;
        public static void CreateProcess()
        {
            uistart = Interprocess.GetKey("ui_start");
            uicallback = Interprocess.GetKey("ui_callback");
            text_path = Interprocess.GetKey("ui_text_path");
            type = Interprocess.GetKey("ui_type");
            body_wait = Interprocess.GetKey("ui_wait_body");
            readyCallback = Task.Run(ProcessConnections);
            render = new Process();
            render.StartInfo.FileName = path;
            render.StartInfo.Arguments = $"-port {Network.Network.UIInputPort} -callback {Network.Network.UICallbackPort}";
            render.Start();
        }
        static async Task ProcessConnections()
        {
            Task wait = uistart.WaitToChange();
            wait.Wait();
            Logger.Logger.Write("KarenRender ready for render!");
            switch (uistart.Value)
            {
                case "ready":
                    IsReady = true;
                    break;
            }
        }

        public static async Task Say(string message, bool wait = false)
        {///*
            if (render == null)
                return;
            if (IsReady == false)
                await readyCallback;//*/
            Interprocess.SetKey("ui_type", "write");
            Interprocess.SetKey("ui_text_path", message);
            Interprocess.SetKey("ui_wait_body", wait.ToString().ToLower());
            await uicallback.WaitToChange();
        }
    }
}
