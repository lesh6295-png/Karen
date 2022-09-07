using System;
using System.IO;
using System.Collections.Generic;

namespace Karen.KBL
{
    public class BinaryLibary
    {
        public int LibaryId = 0;

        public int Count = 0;
        public BinaryItem[] items;

        FileStream file;
        int headerLength = 0;
        public BinaryLibary(string libaryPath)
        {
            if (!File.Exists(libaryPath))
                throw new FileNotFoundException("Karen Binary Libary not found.");
            file = File.Open(libaryPath, FileMode.Open);
            ReadHeader();
        }

        private void ReadHeader()
        {
            //Read count of files in libary
            byte[] countbin = new byte[4];
            file.Read(countbin, 0, 4);
            Count = BitConverter.ToInt32(countbin, 0);

            List<BinaryItem> items = new List<BinaryItem>();
            for(int i = 0; i < Count; i++)
            {
                byte[] item = new byte[20];
                file.Read(item, 0, 20);

            }
        }
    }
    /// <summary>
    /// 20 bytes - size struct in lib
    /// </summary>
    public class BinaryItem
    {
        public long Addres = 0, Length = 0;
        public int Id=0;
    }
}
