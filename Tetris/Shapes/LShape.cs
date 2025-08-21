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
    internal class LShape : Shape
    {
        public LShape()
        {
            transform = new Transform(new Size(3, 2));
            shapeType = ShapeType.LShape;

            this.grid = new int[,]{
                { 2, 0},
                { 2, 0},
                { 2, 2},
            };
        }
    }
}
