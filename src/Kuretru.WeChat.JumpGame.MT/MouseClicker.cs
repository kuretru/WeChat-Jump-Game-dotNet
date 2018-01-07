using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Kuretru.WeChat.JumpGame.MT
{
    public class MouseClicker
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;

        private Point _point;
        private int _time;
        
        public void Click(Point point, int millisecond)
        {
            _point = point;
            _time = millisecond;
            Thread t = new Thread(new ThreadStart(MouseDown));
            t.Start();
        }

        private void MouseDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)_point.X, (uint)_point.Y, 0, 0);
            Thread.Sleep(_time);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)_point.X, (uint)_point.Y, 0, 0);
        }
    }
}
