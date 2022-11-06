// yes this code is bad

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Karen.Types;
namespace Karen.Engine.Scripting
{
    public class VariableContext
    {
#if RELEASE
#else
        public
#endif
        List<Variable> variables = new();
        public string Guid { get; private set; }
        public VariableContext()
        {
            Guid = Extensions.RandomString();
        }
        public Variable Get(string name, bool throwifnull = true, bool create = false)
        {
            foreach (var q in variables)
                if (q.name == name)
                    return q;
            if(throwifnull)
                throw new ObjectNotFoundException($"In {Guid} context variable with {name} dont exist.");
            if (create)
            {
                Variable newv = new Variable(name, null);
                Add(newv);
                return newv;
            }
            return null;
        }
        public void Add(Variable variable, bool checkname=false)
        {
            if (checkname)
            {
                for(int i = 0; i < variables.Count; i++)
                {
                    if (variables[i].name == variable.name)
                    {
                        throw new DublicateObjectException($"Variable with {variable.name} alredy exists in {Guid} context.");
                    }
                }
            }
            variables.Add(variable);
        }
    }
}
