// yes this code is bad

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Karen.Types;
using Karen.Logger;
namespace Karen.Engine
{
    public class VariableContext
    {
        List<kint16> kshort = new List<kint16>();
        List<kint32> kint = new List<kint32>();
        List<kint64> klong = new List<kint64>();
        List<kuint16> kushort = new List<kuint16>();
        List<kuint32> kuint = new List<kuint32>();
        List<kuint64> kulong = new List<kuint64>();
        List<kdecimal> kdecimals = new List<kdecimal>();
        List<kdouble> kdoubles = new List<kdouble>();
        List<kstring> kstrings = new List<kstring>();

        public string Guid { get; private set; }
        public int TotalContextSize()
        {
            return kshort.Count + kint.Count + klong.Count + kushort.Count + kulong.Count + kuint.Count + kdecimals.Count + kdoubles.Count + kstrings.Count;
        }
        public VariableContext()
        {
            Guid = Extensions.RandomString();
        }
        /// <summary>
        /// Try to return variable by name. 
        /// Find start in kint16 -> kint32 -> kint64 -> kuint16 -> kuint32 -> kuint64 -> kdouble -> kdecimal -> kstring 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object Get(string name)
        {
            object result = null;
            kshort.ForEach(x => { if (x.name == name) result = x; });
            kint.ForEach(x => { if (x.name == name) result = x; });
            klong.ForEach(x => { if (x.name == name) result = x; });
            kushort.ForEach(x => { if (x.name == name) result = x; });
            kuint.ForEach(x => { if (x.name == name) result = x; });
            kulong.ForEach(x => { if (x.name == name) result = x; });
            kdoubles.ForEach(x => { if (x.name == name) result = x; });
            kdecimals.ForEach(x => { if (x.name == name) result = x; });
            kstrings.ForEach(x => { if (x.name == name) result = x; });
            return result;
        }
        //add variable to context
        public void Add(kint16 variable)
        {
            kshort.Add(variable);
        }
        public void Add(kint32 variable)
        {
            kint.Add(variable);
        }
        public void Add(kint64 variable)
        {
            klong.Add(variable);
        }
        public void Add(kuint16 variable)
        {
            kushort.Add(variable);
        }
        public void Add(kuint32 variable)
        {
            kuint.Add(variable);
        }
        public void Add(kuint64 variable)
        {
            kulong.Add(variable);
        }
        public void Add(kdouble variable)
        {
            kdoubles.Add(variable);
        }
        public void Add(kdecimal variable)
        {
            kdecimals.Add(variable);
        }
        public void Add(kstring variable)
        {
            kstrings.Add(variable);
        }

        public void Add(object variable)
        {
            if (variable is kint16 q)
                Add(q);
            if (variable is kint32 qq)
                Add(qq);
            if (variable is kint64 qw)
                Add(qw);
            if (variable is kuint16 qww)
                Add(qww);
            if (variable is kuint32 qe)
                Add(qe);
            if (variable is kuint64 eq)
                Add(eq);
            if (variable is kdouble rrq)
                Add(rrq);
            if (variable is kdecimal fq)
                Add(fq);
            if (variable is kstring heq)
                Add(heq);

            
        }
    }
}
