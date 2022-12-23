using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Karen.Types;
using MessagePack;
using System.IO;
namespace Karen.Locale
{
    public  class SourceManager : IManagerSerializator
    {
        public static SourceManager Singelton;
        static SourceManager()
        {
            Singelton = new();
        }
         List<KeySource> sources = new();
        Dictionary<int, string> loaded = new();
        public  int LoadSource(string path, bool atl=true)
        {
            KeySource news = new KeySource(path);
            sources.Add(news);
            if(atl)
            loaded.Add(news.sourceid, path);
            return news.sourceid;
        }
        public int LoadSourceUnsafe(string path, bool atl = true)
        {
            try
            {
                KeySource news = new KeySource(path);
                sources.Add(news);
                if (atl)
                    loaded.Add(news.sourceid, path);
                return news.sourceid;
            }
            catch
            {
                return -1;
            }
        }
        public  void UnloadSource(int id)
        {
            for(int i = 0; i < sources.Count; i++)
            {
                if (sources[i].sourceid == id)
                {
                    sources.Remove(sources[i]);
                    loaded.Remove(id);
                }
            }
        }
        public  string ExtractTranslate(string key, int sourceid = -1)
        {
            if (sourceid != -1)
            {
                foreach(var q in sources)
                {
                    if (q.sourceid == sourceid)
                    {
                        return q.TryExtractTranslate(key);
                    }
                }
            }
            foreach(var q in sources)
            {
                string w = q.TryExtractTranslate(key);
                if (w == key)
                {
                    continue;
                }
                return w??key;
            }

            return key;
        }

        public void Load()
        {
            byte[] raw = File.ReadAllBytes(targetfolder + "locales");
            loaded = MessagePackSerializer.Deserialize<Dictionary<int,string>>(raw);

            var k = loaded.Keys;
            foreach(var q in k)
            {
                string s;
                if (loaded.TryGetValue(q, out s))
                    LoadSource(s,false);
            }
        }

        public void Save()
        {
            byte[] sl = MessagePackSerializer.Serialize<Dictionary<int, string>>(loaded, options: new MessagePackSerializerOptions(MessagePack.Resolvers.StandardResolverAllowPrivate.Instance));
            File.WriteAllBytes(targetfolder + "locales", sl);
        }

        public void SetFolder(string folder)
        {
            targetfolder = folder;
        }
        string targetfolder = "";
    }
}
