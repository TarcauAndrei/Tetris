using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Tetris.Main;
using Size = Tetris.Main.Size;
using Point = Tetris.Main.Point;

namespace Tetris.Shapes
{
    internal class Shape : Grid
    {
        protected ShapeType shapeType;

        public Shape()
        {
            transform = new Transform();
        }

        public Shape(Shape shape)
        {
            transform = new Transform(shape.transform);
            grid = (int[,])shape.grid.Clone();
        }

        public Shape(Shape shape, Point position)
        {
            transform = new Transform(position, shape.transform.size);
            grid = (int[,])shape.grid.Clone();
        }
    }
}
