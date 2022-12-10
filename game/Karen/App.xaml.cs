using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

using Karen.Engine;
namespace Karen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public Thread Engine { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            Config.ParceArgs(e.Args);
            
            //Create dedicated thread to engine
            Engine = new Thread(()=> { EngineStarter.Start(); });
            Engine.Start();
            
            base.OnStartup(e);
        }
    }
}
