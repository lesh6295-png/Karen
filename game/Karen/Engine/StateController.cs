using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Karen.Engine.Scripting;
using System.Runtime.Serialization.Formatters.Binary;
using MessagePack;
namespace Karen.Engine
{
    public static class StateController
    {
        public static async void Enable()
        {

        }

        public static async void SerialiazeJson()
        {
            string m = System.Text.Json.JsonSerializer.Serialize<VirtualMachine>(EngineStarter.VM, new System.Text.Json.JsonSerializerOptions { IncludeFields = true });
            File.WriteAllText(Karen.Registry.RegController.GetKarenFolderPath() + "vm.pos", m);
        }
        public static async void Serialiaze()
        {
            byte[] m = MessagePackSerializer.Serialize<VirtualMachine>(EngineStarter.VM);
            File.WriteAllBytes(Karen.Registry.RegController.GetKarenFolderPath() + "vm.b", m);
        }
    }
}
