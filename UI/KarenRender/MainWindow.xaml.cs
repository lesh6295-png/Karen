// yes i known what this code is bad
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Karen.WinApi;
using System.Threading;
using System.Net.Http;
using System.Net;

namespace KarenRender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpListener listerner;
        IntPtr mainHandle;
        HttpClient callback;
        bool wait = false;
        public MainWindow()
        {
            callback = new HttpClient();
            InitializeComponent();
            listerner = new HttpListener();
            listerner.Prefixes.Add($"http://localhost:{App.Port}/");
            mainHandle = new WindowInteropHelper(this).Handle;
            Task.Run(ProcessInputConnections);
        }

        async Task ProcessInputConnections()
        {
            listerner.Start();
            Task.Run(SendReadyRequest).Wait(6000);
            while (true)
            {
                var con = await listerner.GetContextAsync();
                string type = con.Request.QueryString["type"];

                byte[] responce;
                switch (type)
                {
                    case "online":
                        responce = Encoding.UTF8.GetBytes($"1");
                        break;
                    case "write":
                        WindowMax();
                        Task prtTask = Write(con.Request.QueryString["text"]);
                        string w = con.Request.QueryString["wait"] ?? "false";
                        wait = true;
                        if (w == "true")
                        {
                            prtTask.Wait();
                        }
                        callback.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{App.CallbackPort}/"));
                        responce = Encoding.UTF8.GetBytes($"1");
                        break;
                    case "sprite":
                        //if body=true we change body component, else - emotion
                        try
                        {
                            bool isbody = Convert.ToBoolean(con.Request.QueryString["body"]);
                            string localPath = Encoding.UTF8.GetString(Convert.FromHexString(con.Request.QueryString["path"]));
                            if (isbody)
                            {
                                Dispatcher.Invoke(() => { body.Source = new BitmapImage(new Uri(localPath)); });
                            }
                            else
                            {
                                Dispatcher.Invoke(() => { emotion.Source = new BitmapImage(new Uri(localPath)); });
                            }
                            responce = Encoding.UTF8.GetBytes("1");
                        }
                        catch
                        {
                            responce = Encoding.UTF8.GetBytes("0");
                        }
                        break;
                    default:
                        responce = Encoding.UTF8.GetBytes($"0");
                        break;
                }

                con.Response.ContentLength64 = responce.Length;
                con.Response.OutputStream.Write(responce, 0, responce.Length);
                con.Response.Close();
            }
        }
        public async Task Write(string str)
        {
            //clear textbox
            Dispatcher.Invoke(() => { textBox.Text = ""; });
            for (int i = 0; i < str.Length; i++)
            {
                Dispatcher.Invoke(() => { textBox.Text += str[i]; });
                if(wait)
                await Task.Delay(App.TextBoxCharTimeOut);
            }
        }
        void WindowMax()
        {
            Input.TopWindow(mainHandle);
        }
        async Task SendReadyRequest()
        {
            await callback.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"http://localhost:{App.CallbackPort}/?type=ready"));
        }
        private void KarenWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (wait)
                    wait = false;
                this.DragMove();
            }
        }

        private void body_KeyUp(object sender, KeyEventArgs e)
        {
            wait = false;
        }

        private void emotion_KeyUp(object sender, KeyEventArgs e)
        {
            wait = false;
        }

        private void Rectangle_KeyUp(object sender, KeyEventArgs e)
        {
            wait = false;
        }

        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            wait = false;
        }
    }
}
