using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using Karen.Types;
using Karen.KBL;
namespace Karen.Engine
{
    /// <summary>
    /// This class cached all used resources in memory (now only sprites)
    /// </summary>
    public static class Cache
    {
        public static TimeSpan ObjectLiveTime = TimeSpan.FromMinutes(5);
        static List<CachedObject> obj = new List<CachedObject>();
        public static BitmapImage GetSprite(int kblId, int fileId)
        {
            string gi = kblId + "-" + fileId;
            foreach (var q in obj)
                if (q is CachedSprite w)
                    if (w.spriteid == gi)
                        return w.Sprite;
            CachedSprite news = new CachedSprite(kblId, fileId);
            obj.Add(news);
            return news.Sprite;
        }
        static Cache()
        {
            CacheGarbageCollector();
        }
        static async void CacheGarbageCollector()
        {
            while (true)
            {
                await Task.Delay(5000);
                for (int i = 0; i < obj.Count; i++)
                {
                    CachedObject q = obj[i];
                    if (DateTime.Now - q.lastAppeal > ObjectLiveTime)
                    {
                        obj.Remove(q);
                    }
                }
                GC.Collect();
            }
        }
    }
}
namespace Karen.Types
{
    public class CachedSprite : CachedObject
    {
        BitmapImage LoadImage(byte[] arr)
        {
            if (arr == null || arr.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(arr))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
        BitmapImage im;
        public string spriteid { get; private set; }
        public BitmapImage Sprite
        {
            set
            {
                im = value;
            }
            get
            {
                lastAppeal = DateTime.Now;
                return im;
            }
        }
        public CachedSprite(int kbl, int file)
        {
            lastAppeal = DateTime.Now;
            spriteid = kbl + "-" + file;
            im = LoadImage(BinaryManager.Singelton.Extract(kbl, file));
        }
    }
}
