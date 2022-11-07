using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Karen.Locale
{
    public class KeySource
    {
        public int sourceid { get; private set; }

        long[] address;
        int count;
        FileStream file;
        int headerlength = 0;
        Key[] keys;
        public string TryExtractTranslate(string key)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (keys[i].key == key)
                    return keys[i].GetTranslate();
            }
            return null;
        }
        long GetLength(int fileindex)
        {
            if (fileindex + 1 == address.Length)
            {
                return file.Length - address[fileindex];
            }
            return address[fileindex + 1] - address[fileindex];
        }
        public KeySource(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Locale file not found.");
            file = File.Open(path, FileMode.Open);
            byte[] id = new byte[8];
            file.Read(id, 0, 4);
            sourceid = BitConverter.ToInt32(id, 0);
            file.Read(id, 0, 4);
            count = BitConverter.ToInt32(id, 0);
            headerlength = 8 + count * 8;
            address = new long[count];
            keys = new Key[count];
            for (int i = 0; i < count; i++)
            {
                file.Read(id, 0, 8);
                address[i] = BitConverter.ToInt64(id, 0);
            }
            for (int i = 0; i < count; i++)
            {
                byte[] key = new byte[GetLength(i)];
                file.Position = address[i];
                file.Read(key, 0, key.Length);
                keys[i] = new Key(Encoding.UTF8.GetString(key));
            }
        }

        internal KeySource(string path, int id, int count, Key[] translations)
        {
            file = File.Open(path, FileMode.Create);
            byte[] buf = BitConverter.GetBytes(id);
            file.Write(buf, 0, 4);
            buf = BitConverter.GetBytes(count);
            file.Write(buf, 0, 4);
            long endheader = 8 + count * 8;
            //write headers
            for (int i = 0; i < count; i++)
            {
                buf = BitConverter.GetBytes(endheader);
                file.Write(buf, 0, 8);
                endheader += translations[i].GetLength();
            }
            //write translations
            for (int i = 0; i < count; i++)
            {
                buf = Encoding.UTF8.GetBytes(translations[i].ToString());
                file.Write(buf, 0, buf.Length);
            }
            file.Close();
        }
    }

    public class KeySourceFactory
    {
        //you dont need this code in final game build
        public static void GenerateTestSource(string path)
        {
            List<Key> k = new List<Key>();
            k.Add(new Key("{key::test;en::Hi!;ru::Привет!;jp::こんにちわ!}"));
            k.Add(new Key("{key::yes;en::Yes;ru::Да;jp::はい}"));
            k.Add(new Key("{key::no;en::No;ru::Нет;jp::いいえ}"));
            new KeySource(path, 5, 3, k.ToArray());
        }
        public static void CreateKeySource(string path, int id, params Key[] keys)
        {
            new KeySource(path, id, keys.Length, keys);
        }
    }
}
