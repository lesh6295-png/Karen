using System;

using Microsoft.Win32;

using Karen.Types;
namespace Karen.Registry
{
    public static class RegController
    {
        /// <summary>
        /// Only on Windows
        /// </summary>
        /// <returns></returns>
        public static ClientState GetState()
        {
            #pragma warning disable CA1416
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey softwareKey = key.CreateSubKey("SOFTWARE");
            RegistryKey karenKey = softwareKey.CreateSubKey("Karen");
            object state = karenKey.GetValue("STATE_INT");
#pragma warning restore
            if (state == null)
                return ClientState.NotInstalled;
            return (ClientState)state;
        }

        /// <summary>
        /// Only on Windws
        /// </summary>
        /// <param name="state"></param>
        public static void WriteState(ClientState state)
        {
#pragma warning disable CA1416
            RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
            RegistryKey softwareKey = key.CreateSubKey("SOFTWARE");
            RegistryKey karenKey = softwareKey.CreateSubKey("Karen");
            karenKey.SetValue("STATE_INT", (int)state);
#pragma warning restore
        }
    }
}
