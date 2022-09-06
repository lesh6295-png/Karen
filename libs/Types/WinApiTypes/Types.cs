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
        public int X, Y;
        public static POINT Empty{
            get
            {
                POINT p;
                p.X = 0;
                p.Y = 0;
                return p;
            }
            }
        public POINT(int x,int y)
        {
            X = x;
            Y = y;
        }
        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }
}
