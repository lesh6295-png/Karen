using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
using Karen.Types;
using System.IO;
using System.Reflection;
namespace Karen.Engine.Scripting
{
    [Serializable]
    [MessagePackObject(keyAsPropertyName: true)]
    public class ScriptContext
    {
        public string[] codelines;
        [IgnoreMember]
        Type api;
        
        public bool isLoad = false;
        public Karen.Types.Guid Guid { get; private set; }
        [IgnoreMember]
        VirtualMachine host;
        public VariableContext localContext;
        public int activeline = 0;
        public List<Label> labels = new List<Label>();
        public bool KillAfterEnd { get; set; } = true;
        public (string from, string to) ifskipper=new();
        public ScriptContext(VirtualMachine host)
        {
            this.host = host;
            Guid = new();
            localContext = new VariableContext();
            Logger.Write($"New Script Context Thread: Guid: {Guid}; local variable context: {localContext.Guid}");
            api = Type.GetType("Karen.Engine.Api.Api", true);
            
        }
        public ScriptContext()
        {
            host = EngineStarter.VM;
            api = Type.GetType("Karen.Engine.Api.Api", true);
        }
        public void SetIfSkip(string from, string to)
        {
            ifskipper.from = '@' + from;
            ifskipper.to = to;
        }
        public void ToLabel(string labelname)
        {
            for(int i = 0; i < labels.Count; i++)
            {
                if (labels[i].name == labelname)
                {
                    activeline = labels[i].position;
                    return;
                }
            }
            throw new Karen.Types.ObjectNotFoundException("Label not found.");
        }
        public void PreExcecute()
        {
            //find labels
            for(int i = 0; i < codelines.Length; i++)
            {
                if (codelines[i] == "")
                    continue;
                if (codelines[i][0] == '@')
                {
                    Label l;
                    l.position = i;
                    l.name = codelines[i].Substring(1);
                    labels.Add(l);
                }
            }
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
        public async void ExcecuteAsync()
        {
            if(!isLoad)
                return;
            PreExcecute();
            for (; activeline < codelines.Length; activeline++)
            {
                if (codelines[activeline] == "")
                    continue;
                string[] com = codelines[activeline].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (com[0] == ifskipper.from)
                {
                    ToLabel(ifskipper.to);
                }
                if (com[0].StartsWith('@'))
                {
                    //check if this is a label
                    continue;
                }
                MethodInfo apiendpoint = api.GetMethod(com[0], BindingFlags.Static | BindingFlags.Public) ?? api.GetMethod('_' + com[0], BindingFlags.Static | BindingFlags.Public);
                if (apiendpoint == null)
                {
                    throw new NullReferenceException("Method not found.");
                }
                List<object> a = SVP(com.Skip(1).Cast<object>().ToList());
                a.Add((object)(new object[] {localContext, Guid, host, this }));
                await (Task)apiendpoint.Invoke(null, new object[] { a.ToArray() }) ;
            }
            if (KillAfterEnd)
                Kill();
        }
        void Kill()
        {
            host.DeleteScriptThread(Guid);
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
