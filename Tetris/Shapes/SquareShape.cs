using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

using Tetris.Main;
using Size = Tetris.Main.Size;

namespace Tetris.Shapes
{
    internal class SquareShape : Shape
    {
        public SquareShape()
        {
            transform = new Transform(new Size(2, 2));
            shapeType = ShapeType.Square;

            this.grid = new int[,]{
                { 2, 2},
                { 2, 2}
            };
        }
    }
}
