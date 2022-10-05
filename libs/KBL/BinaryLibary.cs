using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace Karen.KBL
{
    public class BinaryLibary
    {
        private int libaryId = 0;

        private int count = 0;
        BinaryItem[] items;

        FileStream file;
        int headerLength = 0;

        public int LibaryId { get => libaryId; private set => libaryId = value; }
        public int Count { get => count; private set => count = value; }

        public BinaryLibary(string libaryPath)
        {
            if (!File.Exists(libaryPath))
                throw new FileNotFoundException("Karen Binary Libary not found.");
            file = File.Open(libaryPath, FileMode.Open);
            ReadHeader();
        }
        internal BinaryLibary(string libaryPath, int countOfFiles, int libaryId, params InputValue[] files)
        {
            DateTime start = DateTime.Now;
            if (files.Length == 0)
                throw new ArgumentException("Empry input list");
            File.Delete(libaryPath);
            file = File.Open(libaryPath, FileMode.CreateNew);
            LibaryId = libaryId;
            Count = countOfFiles;
            byte[] arr = BitConverter.GetBytes(libaryId);
            file.Write(arr, 0, 4);
            arr = BitConverter.GetBytes(count);
            file.Write(arr, 0, 4);
            int address = 8 + (20 * files.Length);

            // list of headers
            List<BinaryItem> items = new List<BinaryItem>();
            foreach (var q in files)
            {
                BinaryItem it = new BinaryItem();
                it.id = q.id;
                it.address = address;
                int lenght = File.ReadAllBytes(q.path).Length;
                it.length = lenght;
                items.Add(it);
                address += lenght;
            }

            //write header
            foreach (var q in items)
            {
                byte[] buf = BitConverter.GetBytes(q.Address);
                file.Write(buf, 0, 8);
                buf = BitConverter.GetBytes(q.Length);
                file.Write(buf, 0, 8);
                buf = BitConverter.GetBytes(q.Id);
                file.Write(buf, 0, 4);
            }

            //write files
            foreach (var q in files)
            {
                byte[] f = File.ReadAllBytes(q.path);
                file.Write(f, 0, f.Length);
            }

            file.Close();
            Console.WriteLine($"Sussesful! Id: {libaryId}, Files: {countOfFiles}, Time to build: {DateTime.Now - start}");
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
            for (int i = 0; i < Count; i++)
            {
                byte[] item = new byte[20];
                file.Read(item, 0, 20);
                BinaryItem q = new();
                q.address = BitConverter.ToInt64(item, 0);
                q.length = BitConverter.ToInt64(item, 8);
                q.id = BitConverter.ToInt32(item, 16);
                items.Add(q);
                headerLength += 20;
            }
            this.items = items.ToArray();
        }

        public byte[] Extract(int id)
        {
            foreach (var i in items)
                if (i.Id == id)
                {
                    byte[] res = new byte[i.Length];
                    file.Position = i.Address;
                    file.Read(res, 0, Convert.ToInt32(i.Length));
                    return res;
                }
            throw new ArgumentException("Invalid id.");
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
        internal int id = 0;
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
    internal struct InputValue
    {
        public int id;
        public string path;
        public override string ToString()
        {
            return $"{id} {path}";
        }
    }
    public class LibaryBuilder
    {
        public LibaryBuilder(string[] args)
        {
            Random r = new Random();
            string libpath = "";
            int libaryid = 0;
            bool randomid = args.Contains("-randomid");
            int itemnumber = 0;
            List<InputValue> l = new();
            for (int i = 0; i < args.Length; i++)
            {
                string parC = args[i];
                switch (parC)
                {
                    case "-libid":
                        libaryid = Convert.ToInt32(args[i + 1]);
                        i++;
                        break;
                    case "-libpath":
                        libpath = (args[i + 1]);
                        i++;
                        break;
                    case "-dir":
                        string[] filn = Directory.GetFiles(args[i + 1]);
                        i++;
                        foreach (var q in filn)
                        {
                            InputValue iv;
                            iv.path = q;
                            iv.id = GetId();
                            l.Add(iv);
                        }
                        break;
                    case "-randomid":

                        break;
                    default:
                        InputValue inp;
                        inp.path = args[i];
                        int res = 0;
                        if (int.TryParse(args[i + 1], out res))
                        {
                            i++;
                        }
                        else
                        {
                            res = GetId();
                        }
                        inp.id = res;
                        l.Add(inp);

                        break;
                }
            }
#if DEBUG
            string kbldecr = $"{libaryid} {libpath}\n\nitems:\n\n";
            l.ForEach(x => kbldecr += x.ToString() + '\n');
            File.WriteAllText(libpath + ".txt", kbldecr);
#endif
            new BinaryLibary(libpath, l.Count, libaryid, l.ToArray());



            int GetId()
            {
                if (randomid)
                    return r.Next();
                itemnumber++;
                return itemnumber;
            }
        }
    }
}
