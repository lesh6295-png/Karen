﻿using System;
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

        static bool saveex()
        {
            //TODO: rewrite this
            return File.Exists(Karen.Registry.RegController.GetKarenFolderPath() + "vm");
        }
        static List<IManagerSerializator> managers = new();
        public static bool Enable()
        {
            managers.Add(Karen.Locale.SourceManager.Singelton);
            managers.Add(Karen.KBL.BinaryManager.Singelton);
            managers.Add(EventManager.Singelton);
            SetFolderManagers();
            return saveex();
        }
        public static void LoadSave()
        {
            byte[] vm = File.ReadAllBytes(Karen.Registry.RegController.GetKarenFolderPath() + "vm");
            VirtualMachine mach = MessagePackSerializer.Deserialize<VirtualMachine>(vm);
            DeserialazeManagers();
            EngineStarter.VM = mach;
            mach.StartAllThread();
        }
        public static void SerialazeManagers()
        {
            managers.ForEach((x) => { x.Save(); });
        }
        public static  void DeserialazeManagers()
        {
            managers.ForEach((x) => { x.Load(); });
        }
        public static  void SetFolderManagers()
        {
            managers.ForEach((x) => { x.SetFolder(Karen.Registry.RegController.GetKarenFolderPath()); });
        }
        public static  void SerialiazeVM()
        {
            byte[] m = MessagePackSerializer.Serialize<VirtualMachine>(EngineStarter.VM, options: new MessagePackSerializerOptions(MessagePack.Resolvers.StandardResolverAllowPrivate.Instance));
            File.WriteAllBytes(Karen.Registry.RegController.GetKarenFolderPath() + "vm", m);
        }
    }
}
