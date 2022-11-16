﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;
namespace Karen.Engine.Scripting
{
    [Serializable]
    [MessagePackObject]
    public class VirtualMachine
    {
        [Key(0)]
        public VariableContext globalHeap;
        [Key(1)]
        public List<ScriptContext> threads;
        [Key(2)]
        public EventManager events;
        public VirtualMachine()
        {
            Logger.Write("Create virtual script machine...");
            globalHeap = new VariableContext();
            Logger.Write($"Global Heap Guid: {globalHeap.Guid}");
            threads = new List<ScriptContext>();

            events = new();
        }
        /// <summary>
        /// Initializes new ScriptContext
        /// </summary>
        /// <returns>New ScriptContext Guid</returns>
        public string AddScriptThread()
        {
            ScriptContext scr = new ScriptContext(this);
            threads.Add(scr);
            return scr.Guid;
        }
        internal ScriptContext GetScriptContext(string Guid)
        {
            foreach(var scr in threads)
            {
                if (scr.Guid == Guid)
                    return scr;
            }
            throw new ArgumentException("Invalid Guid.");
        }
    }
}
