using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Main
{
    internal class Size
    {
        public Size()
        { 
            this.y = 0; 
            this.x = 0; 
        }
        public Size(int y, int x) 
        { 
            this.y = y; 
            this.x = x; 
        }
        public Size(Size size)
        {
            this.y = size.y;
            this.x = size.x;
        }

        public int y;
        public int x;

        public void RotateSize()
        {
            int temp = y;
            y = x;
            x = temp;
        }
    }
}
