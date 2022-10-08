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
using Karen.InterProcess;

namespace KarenRender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IntPtr mainHandle;

        InterprocessItem _type, text_path, wait_body;

        bool wait = false;
        public MainWindow()
        {
            InitializeComponent();
            mainHandle = new WindowInteropHelper(this).Handle;
            Task.Run(ProcessInputConnections);
        }

        async Task ProcessInputConnections()
        {
            Interprocess.SetKey("ui_start", "ready");
            _type = Interprocess.GetKey("ui_type");
            text_path = Interprocess.GetKey("ui_text_path");
            wait_body = Interprocess.GetKey("ui_wait_body");
            while (true)
            {
                string type = _type.Value;
                Task[] inputwait = new Task[3];
                inputwait[0] = Task.Run(_type.WaitUpdate);
                inputwait[1] = Task.Run(text_path.WaitUpdate);
                inputwait[2] = Task.Run(wait_body.WaitUpdate);
                string callback = "";
                switch (type)
                {
                    case "online":
                        callback = "online";
                        break;
                    case "write":
                        WindowMax();
                        Task prtTask = Write(text_path.Value);
                        string w = wait_body.Value ?? "false";
                        wait = true;
                        if (w == "true")
                        {
                            prtTask.Wait();
                        }
                        callback = "write";
                        break;
                    case "sprite":
                        //if body=true we change body component, else - emotion
                        try
                        {
                            bool isbody = Convert.ToBoolean(wait_body.Value);
                            string localPath = text_path.Value;
                            if (isbody)
                            {
                                Dispatcher.Invoke(() => { body.Source = new BitmapImage(new Uri(localPath)); });
                            }
                            else
                            {
                                Dispatcher.Invoke(() => { emotion.Source = new BitmapImage(new Uri(localPath)); });
                            }
                            callback = "sprite";
                        }
                        catch
                        {
                            callback = "sprite_fall";
                        }
                        break;
                    default:
                        callback = "unknown";
                        break;
                }
                Interprocess.SetKey("ui_responce", callback);
                Task.WaitAll(inputwait);
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
