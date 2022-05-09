using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace SearchImage.Classes
{
    public struct POINT
    {
        public int X;
        public int Y;
    }

    class CommandMouse
    {
        [DllImport("user32.dll")]
        static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        static extern void SetCursorPos(int x, int y);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(MouseFlags dwFlags, int dx,
            int dy, int dwData, int dwExtraInfo);

        [Flags]
        enum MouseFlags
        {
            Move = 1, LeftDown = 0x02, LeftUp = 0x04, RightDown = 0x08,
            RightUp = 0x10, MiddleDown = 20, MiddleUp = 40, Absolute = 8000
        };

        static public void Move(int x, int y)
        {
            SetCursorPos(x, y);
        }
        static public void MoveTime(int x, int y, int time)
        {
            Stopwatch stopwatch = new Stopwatch();
            GetCursorPos(out POINT p);

            x -= p.X;
            y -= p.Y;

            stopwatch.Start();

            while (stopwatch.Elapsed.TotalMilliseconds < time)
            {
                SetCursorPos(p.X + (int)(stopwatch.Elapsed.TotalMilliseconds / time * x),
                    p.Y + (int)(stopwatch.Elapsed.TotalMilliseconds / time * y));
            }
            stopwatch.Stop();
            SetCursorPos(p.X + x, p.Y + y);
        }

        static public void MoveVector(int dx, int dy)
        {
            mouse_event(MouseFlags.Absolute | MouseFlags.Move, dx, dy, 0, 0);
        }
        static public void MoveVectorTime(int x, int y, int time)
        {
            GetCursorPos(out POINT p);
            MoveTime(p.X + x, p.Y + y, time);
        }

        static public void LeftDown()
        {
            GetCursorPos(out POINT p);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftDown, p.X, p.Y, 0, 0);
        }
        static public void LeftUp()
        {
            GetCursorPos(out POINT p);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftUp, p.X, p.Y, 0, 0);
        }

        static public void LeftClick()
        {
            GetCursorPos(out POINT p);
            mouse_event(MouseFlags.Absolute | MouseFlags.LeftDown | MouseFlags.LeftUp, p.X, p.Y, 0, 0);
        }

        static public void MiddleClick()//Нажимается ещё и ПКМ
        {
            GetCursorPos(out POINT p);
            mouse_event(MouseFlags.Absolute | MouseFlags.MiddleUp | MouseFlags.MiddleDown, p.X, p.Y, 0, 0);
            //RightUp();
        }

        static public void RightClick()
        {
            GetCursorPos(out POINT p);
            mouse_event(MouseFlags.Absolute | MouseFlags.RightDown | MouseFlags.RightUp, p.X, p.Y, 0, 0);
        }

        static public void RightUp()
        {
            GetCursorPos(out POINT p);
            mouse_event(MouseFlags.Absolute | MouseFlags.RightUp, p.X, p.Y, 0, 0);
        }
        static public void RightDown()
        {
            GetCursorPos(out POINT p);
            mouse_event(MouseFlags.Absolute | MouseFlags.RightDown, p.X, p.Y, 0, 0);
        }
    }
}
