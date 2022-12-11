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
                        ChangeDir = true;
                        break;
#if TESTING
                    case "--testing":
                        AUTO_TEST = true;
                        break;
#endif
                    case "--process-event-delay":
                        ProcessUpdateDelay = Convert.ToInt32(q[i++]);
                        break;
                    case "--ignore-save":
                        IgnoreSave = true;
                        break;
                }
            }
        }
#if TESTING
        public static bool AUTO_TEST = false;
#endif
        public static bool LeaveLogs = false;
        public static bool ChangeDir = true;
        public static bool IgnoreSave = false;
        public static int ProcessUpdateDelay = 850;
    }
}
