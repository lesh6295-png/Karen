using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Karen.Types;
using System.IO;
using System.Reflection;
namespace Karen.Engine.Scripting
{
    public class ScriptContext
    {
        #if DEBUG
        public
#endif
        string[] codelines;
        Type api;

        bool isLoad = false;
        public string Guid { get; private set; }
        VirtualMachine host;
#if DEBUG
        public
#endif
        VariableContext localContext;
        public ScriptContext(VirtualMachine host)
        {
            this.host = host;
            Guid = Extensions.RandomString();
            localContext = new VariableContext();
            Logger.Write($"New Script Context Thread: Guid: {Guid}; local variable context: {localContext.Guid}");
            api = Type.GetType("Karen.Engine.Api.Api", true);
            
        }
        [Obsolete]
        public void LoadScriptFromDrive(string path)
        {
            var rawcode = File.ReadAllLines(path);
            List<string> final = new List<string>();
            foreach(var line in rawcode)
            {
                if (line.StartsWith("~"))
                    continue;
                final.Add(line);
            }
            codelines = final.ToArray();
            isLoad = true;
        }
        public void LoadScriptFromByteArray(byte[] arr)
        {
            //clear lines from 0x00 bytes, CR and LF. With 0x00 bytes in string, we can`t get correct api endpoint
            arr = arr.Where(x => { if (x != 0 || x!=0x0d) return true; return false;  }).ToArray();

            string rawcode = Encoding.UTF8.GetString(arr).Replace("\r","")
                //remove U+FEFF symbol. Dont touch this.
                .Replace("﻿", "");
            string[] lines = rawcode.Split("\n");
            List<string> final = new List<string>();
            foreach (var line in lines)
                if (line.StartsWith("~"))
                    continue;
                else
                    final.Add(line);
            codelines = final.ToArray();
            isLoad = true;
        }
        [Obsolete("Use async version")]
        public void Excecute()
        {
            if (!isLoad)
                return;

            for(int i = 0; i < codelines.Length; i++)
            {
                string[] com = codelines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                MethodInfo apiendpoint = api.GetMethod(com[0]);
                if (apiendpoint == null)
                {
                    throw new NullReferenceException("Method not found.");
                }
                object[] a = com.Skip(1).Cast<object>().ToArray();
                ((Task)apiendpoint.Invoke(null, new object[] { a })).Wait();
            }
        }
        public async void ExcecuteAsync()
        {
            if(!isLoad)
                return;

            for (int i = 0; i < codelines.Length; i++)
            {
                string[] com = codelines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                MethodInfo apiendpoint = api.GetMethod(com[0], BindingFlags.Static | BindingFlags.Public);
                if (apiendpoint == null)
                {
                    throw new NullReferenceException("Method not found.");
                }
                List<object> a = SVP(com.Skip(1).Cast<object>().ToList());
                a.Add((object)(new object[] {localContext, Guid, host }));
                await (Task)apiendpoint.Invoke(null, new object[] { a.ToArray() }) ;
            }
        }
        List<object> SVP(List<object> input)
        {
            List<object> res = new ();
            foreach(var q in input)
            {
                string s = (string)q;
                if (s.Contains(":"))
                {
                    string[] a = s.Split(":");
                    if (a[0] == "int")
                    {
                        res.Add(Convert.ToInt32(a[1]));
                        continue;
                    }
                }
                res.Add(q);
            }
            return res;
        }
    }
}
