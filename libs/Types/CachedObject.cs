using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Karen.Types
{
    /// <summary>
    /// Base class to cached object
    /// </summary>
    public abstract class CachedObject
    {
        public DateTime lastAppeal;
        public Guid Guid { get; private set; } = new Guid();
    }
}
