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
    internal class MirrorLShape : Shape
    {
        public MirrorLShape()
        {
            transform = new Transform(new Size(3, 2));
            shapeType = ShapeType.Square;

            this.grid = new int[,]{
                { 0, 2},
                { 0, 2},
                { 2, 2}
            };
        }
    }
}
