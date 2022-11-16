using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Locale
{
    public  class SourceManager
    {
        public static SourceManager Singelton;
        static SourceManager()
        {
            Singelton = new();
        }
         List<KeySource> sources = new();
        public  int LoadSource(string path)
        {
            KeySource news = new KeySource(path);
            sources.Add(news);
            return news.sourceid;
        }
        public  void UnloadSource(int id)
        {
            for(int i = 0; i < sources.Count; i++)
            {
                if (sources[i].sourceid == id)
                {
                    sources.Remove(sources[i]);
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
                return w;
            }

            return key;
        }
    }
}
