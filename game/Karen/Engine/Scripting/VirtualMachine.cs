using System;
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
        public bool AllowHideWindow = true;
        public VirtualMachine()
        {
            Logger.Write("Create virtual script machine...");
            globalHeap = new VariableContext();
            Logger.Write($"Global Heap Guid: {globalHeap.Guid}");
            threads = new List<ScriptContext>();

            new Karen.Engine.SEManager(this);
        }
        /// <summary>
        /// Start all ScriptContext in this VirtualMachine
        /// </summary>
        public void StartAllThread()
        {
            threads.ForEach((x) => { x.ExcecuteAsync(); });
        }
        /// <summary>
        /// Initializes new ScriptContext
        /// </summary>
        /// <returns>New ScriptContext Guid</returns>
        public Karen.Types.Guid AddScriptThread()
        {
            ScriptContext scr = new ScriptContext(this);
            threads.Add(scr);
            return scr.Guid;
        }
        public void DeleteScriptThread(Types.Guid guid)
        {
            threads.Remove(GetScriptContext(guid));
        }
        internal ScriptContext GetScriptContext(Karen.Types.Guid Guid)
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
