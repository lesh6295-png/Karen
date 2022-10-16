﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Karen.Locale;
using Karen.Types;
using System.IO;
namespace Karen.Engine
{
    internal static class Api
    {
        public static async Task log(object?[]? message)
        {
            StringBuilder sb = new StringBuilder();
            foreach(var q in message)
            {
                if (q != null)
                    if (q is string w)
                        sb.Append(w + " ");
            }
            Logger.Logger.Write(sb.ToString());
        }
        public static async Task newsc(object?[]? par)
        {
            ScriptContext nsc = new ScriptContext((VirtualMachine)(((object[])par.Last())[2]));
            if(par[0] is string q)
            {
                nsc.LoadScriptFromDrive(q);
                nsc.ExcecuteAsync();
            }
        }
        public static async Task wait(object?[]? par)
        {
            int time = Convert.ToInt32((string)par[0]);
            await Task.Delay(time);
        }
        public static async Task var(object?[]? par)
        {
            VariableContext local = (VariableContext)(((object[])par.Last())[0]);
            kvar newvar = new kvar((VariableType)Enum.Parse(typeof(VariableType), par.TryExtractElement<object, string>("Int32",0),true), par.TryExtractElement<object, string>("newvariable",1), par[2]);
            if (par.Length >= 5)
            {
                string target = (string)par[3];
                if(target == "global")
                {
                    ((VirtualMachine)(((object[])par.Last())[2])).globalHeap.Add(newvar);
                    return;
                }
                
            }
            local.Add(newvar);
        }
        public static async Task quit(object?[]? par)
        {
            int code = par.TryExtractElement<object,int>(0, 0);
            Logger.Logger.Write($"QUIT: code: {code}");
            Environment.Exit(code);
        }
        public static async Task locales(object?[]? par)
        {
            string mode = par.TryExtractElement<object, string>("unk");
            switch (mode)
            {
                case "load":
                    string path = par.TryExtractElement<object, string>("unk", 1);
                    SourceManager.LoadSource(path);
                    break;
                case "unload":
                    int unloadid = par.TryExtractElement<object, int>(-1, 1);
                    SourceManager.UnloadSource(unloadid);
                    break;
                default:
                    throw new InvalidApiParamsException("Unknown locales mode: " + mode);
                    break;
            }
            Logger.Logger.Write($"SourceManager {mode} {par[1].ToString()}");
        }
        public static async Task debug(object?[]? par)
        {
            string mode = par.TryExtractElement<object, string>("unk");
            
            switch (mode)
            {
                case "vm_dump":
                    string dump = System.Text.Json.JsonSerializer.Serialize(((object[])par.Last()).Last(), typeof(VirtualMachine), new System.Text.Json.JsonSerializerOptions { IncludeFields = true, WriteIndented=true});
                    File.WriteAllText("vm_dump.json", dump);
                    break;
            }
        }

        public static async Task say(object?[]? par)
        {
            string text = par.TryExtractElement<object, string>("unk");

            await UIWindow.Say(text);
        }
    }
}