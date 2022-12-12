using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Karen.Types;
using MessagePack;
namespace Karen.KBL
{
    /// <summary>
    /// This class will be help you with controll all KBL and provide Load/Unload and Extract api
    /// </summary>
    public  class BinaryManager : IManagerSerializator
    {
         List<BinaryLibary> sources = new();
        public static BinaryManager Singelton;
        Dictionary<int, string> kbls = new();
        static BinaryManager()
        {
            Singelton = new();
        }
        public int LoadKBL(string path,bool atl=true)
        {
            BinaryLibary news = new BinaryLibary(path);
            if(atl)
            kbls.Add(news.LibaryId, path);
            sources.Add(news);
            return news.LibaryId;
        }
        public  void UnloadKBL(int id)
        {
            for (int i = 0; i < sources.Count; i++)
            {
                if (sources[i].LibaryId == id)
                {
                    sources.Remove(sources[i]);
                    kbls.Remove(id);
                }
            }
        }
        public  byte[] Extract(int libaryid, int fileid)
        {
            foreach (var q in sources)
            {
                if (q.LibaryId == libaryid)
                {
                    return q.Extract(fileid);
                }
            }
            throw new Karen.Types.ObjectNotFoundException("Falled to extract bytes from KBL.");
        }
        /// <summary>
        /// Find kbl with id, return false if no one found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Contains(int id)
        {
            foreach(var q in sources)
            {
                if (q.LibaryId == id)
                {
                    return true;
                }
            }
            return false;
        }
        public void Load()
        {
            byte[] raw = File.ReadAllBytes(targetfolder + "kbl");
            kbls = MessagePackSerializer.Deserialize<Dictionary<int,string>>(raw);
            var k = kbls.Keys;
            foreach (var q in k)
            {
                string s;
                if (kbls.TryGetValue(q, out s))
                    LoadKBL(s,false);
            }
        }

        public void Save()
        {
            byte[] sl = MessagePackSerializer.Serialize<Dictionary<int,string>>(kbls, options: new MessagePackSerializerOptions(MessagePack.Resolvers.StandardResolverAllowPrivate.Instance));
            File.WriteAllBytes(targetfolder + "kbl", sl);
        }

        public void SetFolder(string folder)
        {
            targetfolder = folder;
        }
        string targetfolder = "";
    }
}
