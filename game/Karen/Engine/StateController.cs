using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Karen.Engine.Scripting;
using System.Runtime.Serialization.Formatters.Binary;
using MessagePack;
using Karen.Types;
namespace Karen.Engine
{
    public static class StateController
    {
       static List<IManagerSerializator> managers=new();
        public static async void Enable()
        {
            managers.Add(Karen.Locale.SourceManager.Singelton);
            managers.Add(Karen.KBL.BinaryManager.Singelton);

            SetFolderManagers();
        }
        public static async void SerialazeManagers()
        {
            managers.ForEach((x) => {x.Save(); });
        }
        public static async void DeserialazeManagers()
        {
            managers.ForEach((x) => { x.Save(); });
        }
        public static async void SetFolderManagers()
        {
            managers.ForEach((x) => { x.SetFolder(Karen.Registry.RegController.GetKarenFolderPath()); });
        }
        public static async void SerialiazeJson()
        {
            string m = System.Text.Json.JsonSerializer.Serialize<VirtualMachine>(EngineStarter.VM, new System.Text.Json.JsonSerializerOptions { IncludeFields = true });
            File.WriteAllText(Karen.Registry.RegController.GetKarenFolderPath() + "vm.pos", m);
        }
        public static async void SerialiazeVM()
        {
            byte[] m = MessagePackSerializer.Serialize<VirtualMachine>(EngineStarter.VM);
            File.WriteAllBytes(Karen.Registry.RegController.GetKarenFolderPath() + "vm.b", m);
        }
    }
}
