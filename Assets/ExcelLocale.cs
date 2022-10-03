using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelLibrary;
using System.Threading.Tasks;
using System.Diagnostics;
using Karen.Locale;
namespace Karen.Assets
{
    public class ExcelLocale
    {
        string ExtractValue(string list, int x, int y)
        {
            try
            {
                return file.Sheet(list).Row(y).Cell(x).Value;
            }
            catch { return ""; }
        }
        Workbook file;
        int localescount = 0;
        string[] locales;

        int keycount = 0;

        List<(int, string)> keyid = new();
        public ExcelLocale(string path)
        {
            file = new();
            file.Open("locales.xlsx");
        }

        public void ParceLocales()
        {
            Stopwatch qq = Stopwatch.StartNew();
            localescount = Convert.ToInt32(ExtractValue("main", 2, 1));
            locales = new string[localescount];
            keycount = Convert.ToInt32(ExtractValue("main", 2, 2));
            Console.WriteLine($"Locales: {localescount}, Keys: {keycount}");
            for (int i = 0; i < localescount; i++)
            {
                locales[i] = ExtractValue("main", 2 + i, 3);
            }
            {
                int i = 0;
                while (true)
                {
                    i++;
                    string key = ExtractValue("keys", 1, i);
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        break;
                    }
                    int id = Convert.ToInt32(ExtractValue("keys", 2, i));
                    keyid.Add((id, key));
                }
            }
            keyid.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            List<(string, List<(string, string)>)> localeskeys = new();
            for (int i = 0; i < locales.Length; i++)
            {
                string locale = locales[i];
                List<(string, string)> kvp = new();
                int l = 0;
                while (true)
                {
                    l++;
                    string key = ExtractValue(locale, 1, l) ;
                    if (string.IsNullOrWhiteSpace(key))
                    {
                        break;
                    }
                    string value = ExtractValue(locale, 2,l);
                    kvp.Add((key, value));
                }
                localeskeys.Add((locale, kvp));
            }

            List<(int, List<Key>)> keyswithid = new();
            for (int i = 0; i < keyid.Count; i++)
            {
                int nowid = i;
                var q = keyid.Where(x => x.Item1 == i).Select(x => x.Item2).ToList();

                foreach (var w in q)
                {
                    Key a = Key.Empry;
                    a.key = w;
                    a.translations = GetLocalesTranslatePairs(w);
                    AddKeyToKWI(a, nowid);
                }
            }

            foreach (var q in keyswithid)
            {
                KeySourceFactory.CreateKeySource($"{q.Item1}.locale", q.Item1, q.Item2.ToArray());
            }
            qq.Stop();
            Console.WriteLine($"Locales build in {qq.Elapsed}");


            void AddKeyToKWI(Key k, int id)
            {
                for (int i = 0; i < keyswithid.Count; i++)
                {
                    var q = keyswithid[i];

                    if (q.Item1 == id)
                    {
                        q.Item2.Add(k);
                        keyswithid[i] = q;
                        return;
                    }
                }
                keyswithid.Add((id, new List<Key> { k }));
            }
            Dictionary<string, string> GetLocalesTranslatePairs(string key)
            {
                Dictionary<string, string> res = new();
                foreach (var q in localeskeys)
                {
                    string translation = "";
                    foreach (var w in q.Item2)
                    {
                        if (w.Item1 == key)
                        {
                            translation = w.Item2;
                            break;
                        }
                    }
                    res.Add(q.Item1, translation);
                }
                return res;
            }
        }
    }
}
