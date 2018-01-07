using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuretru.WeChat.JumpGame.Lib
{
    public class DistanceCalculator
    {
        private double _timeCoefficient = 0.00120;

        public DistanceCalculator(double timeCoefficient)
        {
            this._timeCoefficient = timeCoefficient;
        }

        public int CalculateTime(Point current, Point next)
        {
            double distance = Math.Pow((next.X - current.X), 2) + Math.Pow((next.Y - current.Y), 2);
            distance = Math.Sqrt(distance);
            return (int)(distance * _timeCoefficient);
        }
    }
}
