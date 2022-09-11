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

        [DllImport("user32.dll")]
        static extern short GetKeyState(int nVirtKey);
        public static bool IsKeyPressed(KeyCode code)
        {
            short kres = GetKeyState((int)code);
            return kres <= -32767;
        }

        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        public static void SetWindowPosition(IntPtr handler, Vector2 position)
        {
            SetWindowPos(handler, new IntPtr(0), (int)position.x, (int)position.y, 0, 0, 0x0001);
        }
    }
}
