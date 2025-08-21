using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Main
{
    internal class Point
    {
        public Point()
        {
            this.x = 0;
            this.y = 0;
        }
        public Point(double y, double x)
        {
            this.y = y;
            this.x = x;
        }
        public Point(Point point)
        {
            this.y = point.y;
            this.x = point.x;
        }

        public double x;
        public double y;

        public static Point operator +(Point a, Point b) { return new Point(a.y + b.y, a.x + b.x); }
        public static Point operator -(Point a, Point b) { return new Point(a.y - b.y, a.x - b.x); }
    }
}
