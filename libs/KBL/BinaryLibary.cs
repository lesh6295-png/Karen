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
        internal BinaryLibary(string libaryPath, int countOfFiles)
        {

        }
        private void ReadHeader()
        {
            //Read count of files and libary id 
            byte[] idbin = new byte[4];
            file.Read(idbin, 0, 4);
            LibaryId = BitConverter.ToInt32(idbin, 0);
            byte[] countbin = new byte[4];
            file.Read(countbin, 0, 4);
            Count = BitConverter.ToInt32(countbin, 0);
            headerLength += 8;
            List<BinaryItem> items = new List<BinaryItem>();
            for(int i = 0; i < Count; i++)
            {
                byte[] item = new byte[20];
                file.Read(item, 0, 20);
                BinaryItem q = new();
                q.address = BitConverter.ToInt64(item, 0);
                q.length = BitConverter.ToInt64(item, 7);
                q.id = BitConverter.ToInt32(item, 16);
                items.Add(q);
                headerLength += 20;
            }
            this.items = items.ToArray();
        }

        ~BinaryLibary()
        {
            file.Close();
        }
    }
    /// <summary>
    /// 20 bytes - size struct in lib
    /// </summary>
    public class BinaryItem
    {
        internal long address = 0, length = 0;
        internal int id=0;
        public long Address
        {
            get
            {
                return address;
            }
        }
        public long Length
        {
            get
            {
                return length;
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
        }
    }
}
