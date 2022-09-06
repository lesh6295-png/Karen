//please, dont touch this file

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace Karen.Types.WinApiTypes
{
    public struct POINT
    {
        public long x, y;
        public static POINT Empty{
            get
            {
                POINT p;
                p.x = 0;
                p.y = 0;
                return p;
            }
            }
        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }
    }
}
