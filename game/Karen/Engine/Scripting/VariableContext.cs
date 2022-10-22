﻿// yes this code is bad

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
#if DEBUG
        public
#endif
        List<kvar> variables = new();
        public string Guid { get; private set; }
        public VariableContext()
        {
            Guid = Extensions.RandomString();
        }
        public kvar Get(string name)
        {
            foreach (var q in variables)
                if (q.name == name)
                    return q;
            throw new ObjectNotFoundException($"In {Guid} context variable with {name} dont exist.");
        }
        public void Add(kvar variable)
        {
            variables.Add(variable);
        }
    }
}
