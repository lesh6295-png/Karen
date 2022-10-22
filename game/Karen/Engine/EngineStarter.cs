using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Karen.Engine
{
    public static class EngineStarter
    {
        public static void Start()
        {
            AppDomain.CurrentDomain.UnhandledException += Logger.ExceptionLog;

        }
    }
}
