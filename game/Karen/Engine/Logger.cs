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
                File.Delete(dirname + '/' + filename);
                File.Delete(dirname + '/' + errorlogname);
            }
            Directory.CreateDirectory(dirname);
            log = File.Open(dirname + '/' + filename, FileMode.Create);

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
            File.WriteAllText(dirname + "/" + errorlogname, e.ToString());
            log.Close();
            Environment.Exit(e.HResult);
        }
    }

}