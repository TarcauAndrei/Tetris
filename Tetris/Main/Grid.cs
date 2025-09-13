using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Tetris.Main
{
    internal class Grid : GameObject
    {
        protected int[,] grid;

        public Grid()
        {
            this.transform = new Transform();
        }

        public Grid(Size size)
        {
            this.transform = new Transform(size);
            this.grid = new int[size.y, size.x];
        }

        public Grid(int[,] grid)
        {
            this.transform = new Transform();
            this.grid = grid;
        }

        public Grid(Grid grid)
        {
            this.transform = new Transform(grid.transform);
            this.grid = (int[,])grid.grid.Clone();
        }

        public int Get(int y, int x) { return grid[y, x]; }
        public void Set(int y, int x, int value) { grid[y, x] = value; }
        public Size GetSize() { return this.transform.size; }

        public void RotateLeft(double gameGridXSize)
        {
            if ((int)transform.position.x + (int)transform.size.x == (int)gameGridXSize)
            {
                for (int i = transform.size.x; i < transform.size.y; i++) { transform.position.x--; }
            }

            int[,] rotatedGrid = new int[transform.size.x, transform.size.y];
            for (int i = 0; i < transform.size.y; i++)
            {
                for (int j = 0; j < transform.size.x; j++)
                {
                    rotatedGrid[transform.size.x - j - 1, i] = grid[i, j];
                }
            }
            grid = rotatedGrid;
            transform.size.RotateSize();
        }

        public void RotateRight(double gameGridXSize)
        {
            if ((int)transform.position.x + (int)transform.size.x == (int)gameGridXSize)
            {
                for (int i = transform.size.x; i < transform.size.y; i++) { transform.position.x--; }
            }

            int[,] rotatedGrid = new int[transform.size.x, transform.size.y];
            for (int i = 0; i < transform.size.y; i++)
            {
                for (int j = 0; j < transform.size.x; j++)
                {
                    rotatedGrid[j, transform.size.y - i - 1] = grid[i, j];
                }
            }
            grid = rotatedGrid;
            transform.size.RotateSize();
        }

        public bool IsInsideGrid(int y, int x)
        {
            return !(y < 0 || y >= GetSize().y || x < 0 || x >= GetSize().x); 
        }

        public Grid Project(Grid grid2)
        {
            Grid result = new Grid(this);

            for (int i = 0; i < grid2.transform.size.y; i++)
            {
                for (int j = 0; j < grid2.transform.size.x; j++)
                {
                    if (IsInsideGrid(i + (int)grid2.transform.position.y, j + (int)grid2.transform.position.x))
                    {
                        if (grid2.Get(i, j) != 0)
                        {
                            result.Set(i + (int)grid2.transform.position.y, j + (int)grid2.transform.position.x, grid2.Get(i, j));
                        }
                    }
                }
            }

            return result;
        }
    }
}
