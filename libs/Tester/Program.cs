using System;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Karen.Registry.RegController.WriteState(Karen.Types.ClientState.Installed);
        }
    }
}
