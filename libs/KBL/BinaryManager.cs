using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.KBL
{
    /// <summary>
    /// This class will be help you with controll all KBL and provide Load/Unload and Extract api
    /// </summary>
    public static class BinaryManager
    {
        static List<BinaryLibary> sources = new();

        public static int LoadKBL(string path)
        {
            BinaryLibary news = new BinaryLibary(path);
            sources.Add(news);
            return news.LibaryId;
        }
        public static void UnloadKBL(int id)
        {
            for (int i = 0; i < sources.Count; i++)
            {
                if (sources[i].LibaryId == id)
                {
                    sources.Remove(sources[i]);
                }
            }
        }
        public static byte[] Extract(int libaryid, int fileid)
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
    }
}
