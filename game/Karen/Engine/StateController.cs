using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Karen.Engine.Scripting;
using System.Runtime.Serialization.Formatters.Binary;
namespace Karen.Engine
{
    public static class StateController
    {
        public static async void Enable()
        {

        }

        public static async void Serialiaze()
        {
            BinaryFormatter f = new BinaryFormatter();
            MemoryStream m = new MemoryStream();
            f.Serialize(m, EngineStarter.VM);
            m.Position = 0;
            File.WriteAllBytes(Karen.Registry.RegController.GetKarenFolderPath() + "\\vm.pos", m.ToArray());
        }
    }
}
