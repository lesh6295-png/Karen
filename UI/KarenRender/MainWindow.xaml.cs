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
        public MainWindow()
        {
            InitializeComponent();
            listerner = new HttpListener();
            listerner.Prefixes.Add($"http://localhost:{App.Port}/");
            Task.Run(ProcessInputConnections);
        }

        async Task ProcessInputConnections()
        {
            listerner.Start();
            while (true)
            {
                var con = await listerner.GetContextAsync();
                string type = con.Request.QueryString["type"];

                byte[] responce;
                switch (type)
                {
                    case "online":
                        responce = Encoding.UTF8.GetBytes($"true");
                        break;
                    default:
                        responce = Encoding.UTF8.GetBytes($"Unknown type.");
                        break;
                }

                con.Response.ContentLength64 = responce.Length;
                con.Response.OutputStream.Write(responce, 0, responce.Length);
                con.Response.Close();
            }
        }
        public void Write(string str)
        {

        }
        private void KarenWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
