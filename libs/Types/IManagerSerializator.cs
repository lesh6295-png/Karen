using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public interface IManagerSerializator
    {
        public void Load();
        public void Save();
        public void SetFolder(string folder);
    }
}
