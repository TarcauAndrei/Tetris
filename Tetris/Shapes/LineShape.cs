using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Tetris.Main;
using Size = Tetris.Main.Size;

namespace Tetris.Shapes
{
    internal class LineShape : Shape
    {
        public LineShape()
        {
            transform = new Transform(new Size(4, 1));
            shapeType = ShapeType.Line;

            this.grid = new int[,]{
                { 2 },
                { 2 },
                { 2 },
                { 2 }
            };
        }
    }
}
