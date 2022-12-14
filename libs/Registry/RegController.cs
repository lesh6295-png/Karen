using System;
using System.IO;
using Microsoft.Win32;

using Karen.Types;
namespace Karen.Registry
{
    public static class RegController
    {
        static RegistryKey cachedRootKaren = null;
        static RegistryKey RootKaren()
        {
#pragma warning disable CA1416
            if (cachedRootKaren == null)
            {
                RegistryKey key = Microsoft.Win32.Registry.CurrentUser;
                RegistryKey softwareKey = key.CreateSubKey("Software");
                cachedRootKaren = softwareKey.CreateSubKey("Karen");
            }
            return cachedRootKaren;
#pragma warning restore
        }
        /// <summary>
        /// Only on Windows
        /// </summary>
        /// <returns></returns>
        public static ClientState GetState()
        {
            RegistryKey karenKey = RootKaren();
            object state = karenKey.GetValue("stateInt");
            if (state == null)
                return ClientState.NotInstalled;
            return (ClientState)state;
        }

        public static bool IsInstalled()
        {
            return GetState() != ClientState.NotInstalled;
        }

        /// <summary>
        /// Only on Windws
        /// </summary>
        /// <param name="state"></param>
        public static void WriteState(ClientState state)
        {
#pragma warning disable CA1416
            RootKaren().SetValue("stateInt", (int)state);
#pragma warning restore
        }

        public static string GetInstallPath()
        {
            return (string)RootKaren().GetValue("installPath", "not_installed");
        }
        public static void SetInstallPath(string path)
        {
            RootKaren().SetValue("installPath", path);
        }

        public static string GetKarenFolderPath()
        {
            return (string)RootKaren().GetValue("karenFolder", "not_exsist");
        }
        public static void SetKarenFolderPath(string path)
        {
            RootKaren().SetValue("karenFolder", path);
        }
        public static void WriteExcRes(string result)
        {
            RootKaren().SetValue("FATAL_EXCEPTION", result);
        }
        public static string GetExcRes()
        {
            return (string)RootKaren().GetValue("FATAL_EXCEPTION");
        }


        
    }
}
