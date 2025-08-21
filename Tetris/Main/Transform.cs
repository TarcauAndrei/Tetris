using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tetris.Main
{
    internal class Transform
    {
        public Transform()
        { 
            this.position = new Point(0, 0); 
            this.size = new Size(0, 0); 
        }
        public Transform(Point position) 
        { 
            this.position = new Point(position); 
            this.size = new Size(0, 0); 
        }
        public Transform(Size size)
        { 
            this.position = new Point(0, 0); 
            this.size = new Size(size);
        }
        public Transform(Point position, Size size) 
        { 
            this.position = new Point(position); 
            this.size = new Size(size); 
        }
        public Transform(Transform transform)
        { 
            this.position = new Point(transform.position);
            this.size = new Size(transform.size);
        }

        public Point position;
        public Size size;
    }
}
