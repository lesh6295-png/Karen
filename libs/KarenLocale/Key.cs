using System;
using System.Collections.Generic;
using Karen.Types;
using System.Text;
using System.Linq;
using System.Collections;
using System.Web;
using MessagePack;
namespace Karen.Locale
{
    [Serializable]
    [MessagePackObject(keyAsPropertyName: true)]
    public struct Key
    {
        public string key;
        /// <summary>
        /// key - language (en, ru, jp), value - translate
        /// </summary>
        public Dictionary<string, string> translations;
        public static Key Empry
        {
            get
            {
                Key k;
                k.key = "default";
                k.translations = new Dictionary<string, string>();
                return k;
            }
        }
        public string GetTranslate()
        {
            string result;
            if (translations.TryGetValue(Localization.Culture, out result))
                    return result.FromHex();
            if (translations.TryGetValue(Localization.DefaultCulture, out result))
                    return result.FromHex();
            return key;
        }
        public int GetLength()
        {
            return Encoding.UTF8.GetByteCount(ToString());
        }
        public override string ToString()
        {
            StringBuilder bl = new StringBuilder();
            bl.Append("{key::");
            bl.Append(key);
            bl.Append(";");
            foreach (var q in translations.Keys)
            {
                bl.Append($"{q}::{translations[q]};");
            }
            bl.Remove(bl.Length - 1, 1);
            bl.Append("}");
            return bl.ToString();
        }


        public Key(string text)
        {
            try
            {
                key = "badkey";
                translations = new Dictionary<string, string>();
                text = text.Replace("{", "").Replace("}", " ");
                var par = text.Split(";");
                foreach (var q in par)
                {
                    string t = q.Trim();
                    var lt = t.Split("::");
                    if (t.Contains("key"))
                    {
                        key = lt[1];
                        continue;
                    }
                    translations.Add(lt[0], lt[1]);
                }
                if (key == "badkey")
                    throw new InvalidKeyStringException("Key not found.");
            }
            catch (Exception e)
            {
                throw new InvalidKeyStringException(e.Message);
            }
        }
    }
}
