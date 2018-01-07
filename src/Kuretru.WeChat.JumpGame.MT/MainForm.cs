using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Kuretru.WeChat.JumpGame.Lib;

namespace Kuretru.WeChat.JumpGame.MT
{
    public partial class MainForm : Form
    {
        private DistanceCalculator _distanceCalculator;
        private IKeyboardMouseEvents _globalHook;
        private MouseClicker _mouseClicker;
        private int _step;
        private Point _lastPoint;

        public MainForm()
        {
            InitializeComponent();
            _mouseClicker = new MouseClicker();
            _step = -1;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (StartButton.Text == "开始")
            {
                StartButton.Text = "结束";
                Start();
            }
            else
            {
                StartButton.Text = "开始";
                Stop();
            }
        }

        private void Log(string text)
        {
            LogTextBox.AppendText(text);
        }

        public void Start()
        {
            Stop();
            _distanceCalculator = new DistanceCalculator(Convert.ToDouble(TimeTextBox.Text));
            _globalHook = Hook.GlobalEvents();
            _globalHook.KeyUp += OnKeyUp;
            _step = 0;
            LogTextBox.Clear();
            Log(string.Format("当前时间系数为:{0}\r\n", TimeTextBox.Text));
            Log("等待点击第一个点:");
        }

        public void Stop()
        {
            if (_globalHook == null)
                return;
            _globalHook.KeyUp -= OnKeyUp;
            _globalHook.Dispose();
            _globalHook = null;
            _distanceCalculator = null;
            _step = -1;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                switch (_step)
                {
                    case 0:
                        _step = 1;
                        _lastPoint = new Point(Cursor.Position.X, Cursor.Position.Y);
                        Log(string.Format("{0},{1}\r\n", _lastPoint.X, _lastPoint.Y));
                        Log("等待点击第二个点:");
                        break;
                    case 1:
                        _step = 0;
                        Point currentPoint = new Point(Cursor.Position.X, Cursor.Position.Y);
                        Log(string.Format("{0},{1}\r\n", currentPoint.X, currentPoint.Y));
                        int time = _distanceCalculator.CalculateTime(currentPoint, _lastPoint);
                        Log(string.Format("按压时间为：{0}ms\r\n", time));
                        _mouseClicker.Click(_lastPoint, time);
                        Log("等待点击第一个点:");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
