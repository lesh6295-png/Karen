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
#if TESTING
        public static bool AUTO_TEST = false;
#endif
        public static bool LeaveLogs = false;
        protected override void OnStartup(StartupEventArgs e)
        {
#if TESTING
            if (e.Args.Contains("--testing"))
                AUTO_TEST = true;
#endif
            if (e.Args.Contains("--leave-logs"))
                LeaveLogs = true;
            //Create dedicated thread to engine
            Engine = new Thread(()=> { EngineStarter.Start(); });
            Engine.Start();
            throw new System.Exception();
            base.OnStartup(e);
        }
    }
}
