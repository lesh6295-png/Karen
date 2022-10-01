﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Karen.Types;
using System.IO;
using System.Reflection;
namespace Karen.Engine
{
    public class ScriptContext
    {
        string[] codelines;
        Type api;
        bool isLoad = false;
        public string Guid { get; private set; }
        VirtualMachine host;
        VariableContext localContext;
        public ScriptContext(VirtualMachine host)
        {
            this.host = host;
            Guid = Extensions.RandomString();
            localContext = new VariableContext();
            Logger.Logger.Write($"New Script Context Thread: Guid: {Guid}; local variable context: {localContext.Guid}");
            api = Type.GetType("Karen.Engine.Api", true);
            
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
                MethodInfo apiendpoint = api.GetMethod(com[0]);
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