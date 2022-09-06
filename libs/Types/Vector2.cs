using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public class Vector2
    {
        public double x = 0, y = 0;
        public Vector2()
        {

        }
        public Vector2(long x, long y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 a, Vector2 b) { return new Vector2 { x = a.x + b.x, y = a.y + b.y }; }
        public static Vector2 operator -(Vector2 a, Vector2 b) { return new Vector2 { x = a.x - b.x, y = a.y - b.y }; }
        public static Vector2 operator *(Vector2 a, Vector2 b) { return new Vector2 { x = a.x * b.x, y = a.y * b.y }; }
        public static Vector2 operator /(Vector2 a, Vector2 b) { return new Vector2 { x = a.x / b.x, y = a.y / b.y }; }
    }
}
