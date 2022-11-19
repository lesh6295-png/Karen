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
        public  int LoadKBL(string path)
        {
            //get short and absolute file name
            // if path="dir\main.miku"
            string shortpath = path.Replace(@"\\", "\\").Split('\\').LastOrDefault();
            //shortpath="main.miku"
            string absolutepath = "";
            try
            {
                absolutepath = Path.GetFullPath(path);
            }
            catch
            {
                absolutepath = null;
            }
            //absolutepath="C:\ProjectFolder\dir222\dir\main.miku"

            //select which path will be used
            string endpoint = path;
            if (absolutepath != null && File.Exists(absolutepath))
                endpoint = absolutepath;
            if (shortpath != null && File.Exists(shortpath))
                endpoint = shortpath;

            BinaryLibary news = new BinaryLibary(endpoint);
            kbls.Add(news.LibaryId, endpoint);
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

        public void Load()
        {
            byte[] raw = File.ReadAllBytes(targetfolder + "kbl");
            kbls = MessagePackSerializer.Deserialize<Dictionary<int,string>>(raw);
            var k = kbls.Keys;
            foreach (var q in k)
            {
                string s;
                if (kbls.TryGetValue(q, out s))
                    LoadKBL(s);
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
