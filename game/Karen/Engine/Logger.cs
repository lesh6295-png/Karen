using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
namespace Karen.Engine
{
    internal static class Logger
    {
        readonly static string dirname = Karen.Registry.RegController.GetKarenFolderPath() + "\\log";
        const string filename = "last.log";
        const string errorlogname = "lasterror.log";
        static FileStream log;
        static Logger()
        {
            if (!App.LeaveLogs)
            {
                //TODO: REWRITE TO File.Exists
                try
                {
                    File.Delete(dirname + '/' + filename);
                    File.Delete(dirname + '/' + errorlogname);
                }
                catch { }
            }
            Directory.CreateDirectory(dirname); 
#if TESTING
            log = File.Open(filename, FileMode.Create);
#else
            log = File.Open(dirname + '/' + filename, FileMode.Create);
#endif
        }
    
        public async static void Write(string message)
        {
            string line = $"[{DateTime.Now.ToString("HH:mm:ss.fff")}] {message}\n";
            byte[] buf = Encoding.UTF8.GetBytes(line);
            await log.WriteAsync(buf, 0, buf.Length);
            await log.FlushAsync();
        }

        public static void ExceptionLog(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Write(e.Message);
#if TESTING
            File.WriteAllText($"{e.HResult}", e.ToString()??e.Message??"Fall to get exception data");
            Karen.Registry.RegController.WriteExcRes(e.ToString());
#else
            File.WriteAllText(dirname + "/" + errorlogname, e.ToString());
#endif
            log.Close();
            Environment.Exit(e.HResult);
        }
    }

}