using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen
{
    public static class Config
    {
        public static void ParceArgs(string[] q)
        {
            for (int i = 0; i < q.Length; i++)
            {
                switch (q[i])
                {
                    case "--leave-logs":
                        LeaveLogs = true;
                        break;
                    case "--disable-change-to-binarys-folder":
                        ChangeDir = false;
                        break;
#if TESTING
                    case "--testing":
                        AUTO_TEST = true;
                        break;
#endif
                    case "--process-event-delay":
                        ProcessUpdateDelay = Convert.ToInt32(q[i++]);
                        break;
                    case "--sep-random":
                        SEPRandomDelay = Convert.ToInt32(q[i++]);
                        break;
                    case "--ignore-save":
                        IgnoreSave = true;
                        break;
                    case "--autoload":
                        Autoload = true;
                        break;
                    case "--locale":
                        i++;
                        Karen.Engine.Logger.Write($"Old locale: {Karen.Locale.Localization.Culture}, new locale: {q[i]}");
                        Karen.Locale.Localization.Culture = q[i];

                        break;
                }
            }
        }
#if TESTING
        public static bool AUTO_TEST = false;
#endif
        public static bool LeaveLogs = false;
        public static bool ChangeDir = true;
        public static bool Autoload = false;
        public static bool IgnoreSave = false;
        public static int ProcessUpdateDelay = 850;
        public static int SEPRandomDelay = 5000;
    }
}
