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
using Karen.Types;
using System.IO;
using System.IO.Pipes;
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

        NamedPipeServerStream server;

        StreamReader reader;
        StreamWriter writer;

        bool wait = false;
        public MainWindow()
        {
            server = new NamedPipeServerStream("nerakuipipe");
            reader = new StreamReader(server);
            writer = new StreamWriter(server);
            InitializeComponent();
            listerner = new HttpListener();
            listerner.Prefixes.Add($"http://localhost:{App.Port}/");
            mainHandle = new WindowInteropHelper(this).Handle;
            Task.Run(ProcessInputConnections);
        }

        async Task ProcessInputConnections()
        {
            server.WaitForConnection();
            while (true)
            {
                var con = IPCData.FromString(reader.ReadLine());
                string type = con.Type;

                string responce;
                switch (type)
                {
                    case "online":
                        responce = "online";
                        break;
                    case "write":
                        WindowMax();
                        Task prtTask = Write((string)con.FirstParam);
                        string w = (string)con.SecondParam ?? "false";
                        wait = true;
                        if (w == "true")
                        {
                            prtTask.Wait();
                        }
                        responce = "write";
                        break;
                    case "sprite":
                        //if body=true we change body component, else - emotion
                        try
                        {
                            bool isbody = Convert.ToBoolean((string)con.SecondParam);
                            string localPath = Encoding.UTF8.GetString(Convert.FromHexString((string)con.FirstParam));
                            if (isbody)
                            {
                                Dispatcher.Invoke(() => { body.Source = new BitmapImage(new Uri(localPath)); });
                            }
                            else
                            {
                                Dispatcher.Invoke(() => { emotion.Source = new BitmapImage(new Uri(localPath)); });
                            }
                            responce = "sprite";
                        }
                        catch
                        {
                            responce = "fall";
                        }
                        break;
                    default:
                        responce = "fall";
                        break;
                }
                writer.WriteLine(responce);
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
