using System;
using System.Runtime.InteropServices;


using Karen.Types.WinApiTypes;
using Karen.Types;
namespace Karen.WinApi
{
    public static class Input
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        public static Vector2 GetMouse()
        {
            POINT p = POINT.Empty;
            GetCursorPos(out p);
            return p.ToVector2();
        }
    }
}
