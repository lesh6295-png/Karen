using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace KarenRender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static  int Port { get; private set; }
        public void ProcessParams(object sender, StartupEventArgs e)
        {
            //default port value
            Port = 1243;


            for (int i = 0; i < e.Args.Length; i++)
            {
                string com = e.Args[i];
                switch (com)
                {
                    case "-port":
                        Port = Convert.ToInt32(e.Args[i + 1]);
                        i++;
                        break;
                }
            }
        }
    }
}
