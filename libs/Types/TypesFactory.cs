//This class - factory to Karen Scripts Variable, like kint32, kdecimal and other
//yes i suck in good coding

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public static class TypesFactory
    {
        public static object BuildVariable(string type, string name, object value)
        {
            object result;
            switch (type)
            {
                case "kint32":
                    kint32 q = kint32.Zero;
                    q.SetValue(value, name);
                    result = q;
                    break;
                case "kint64":
                    kint64 qq = kint64.Zero;
                    qq.SetValue(value, name);
                    result = qq;
                    break;
                case "kint16":
                    kint16 wq = kint16.Zero;
                    wq.SetValue(value, name);
                    result = wq;
                    break;
                case "kuint32":
                    kuint32 eq = kuint32.Zero;
                    eq.SetValue(value, name);
                    result = eq;
                    break;
                case "kuint64":
                    kuint64 qx =kuint64.Zero;
                    qx.SetValue(value, name);
                    result = qx;
                    break;
                case "kuint16":
                    kuint16 cq=kuint16.Zero;
                    cq.SetValue(value, name);
                    result = cq;
                    break;
                case "kdecimal":
                    kdecimal qg=kdecimal.Zero;
                    qg.SetValue(value, name);
                    result = qg;
                    break;
                case "kdouble":
                    kdouble pq=kdouble.Zero;
                    pq.SetValue(value, name);
                    result = pq;
                    break;
                case "kstring":
                    kstring qk=kstring.Empry;
                    qk.SetValue(value, name);
                    result = qk;
                    break;
                default:
                    result = kint32.Zero;
                    break;
            }

            return result;
        }
    }
}
