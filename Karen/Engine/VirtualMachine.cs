using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Engine
{
    public class VirtualMachine
    {
        public VariableContext globalHeap;
        public List<ScriptContext> threads;
        public VirtualMachine()
        {
            Logger.Logger.Write("Create virtual script machine...");
            globalHeap = new VariableContext();
            Logger.Logger.Write($"Global Heap Guid: {globalHeap.Guid}");
            threads = new List<ScriptContext>();
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
