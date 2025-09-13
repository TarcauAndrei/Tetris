using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

using Tetris.Shapes;

using Point = Tetris.Main.Point;
using Shape = Tetris.Shapes.Shape;

namespace Tetris.Main
{
    internal class Game
    {
        private Grid gameGrid;
        private Shape currentShape;
        private Shape nextShape;
        private Shape currentShapeProjection;

        private GameState gameState;
        private int score;

        private double currentFallTime = 1;
        private double fallTime = 1;
        private double finePlaceTime = 0.5;

        public Game()
        {
            this.gameGrid = new Grid(new Size(20, 10));

            currentShape = new Shape();
            nextShape = new Shape();
            currentShapeProjection = new Shape();
            NextShape(); NextShape();

            gameState = GameState.Running;
            score = 0;
        }

        public void Update(double deltaTime)
        {
            if (gameState != GameState.Running) { return; }

            currentFallTime -= deltaTime;
            if (currentFallTime <= 0)
            {
                currentFallTime = fallTime;
                if (CheckShapeCollision(new Shape(currentShape, currentShape.transform.position + new Point(1, 0))) == false)
                {
                    ShapeFall(currentShape);
                }
                else
                {
                    PlaceShape(currentShape);
                    NextShape();
                }
            }

            currentShapeProjection = ProjectShapeDown(currentShape);
        }

        private void ShapeFall(Shape shape)
        {
            shape.transform.position.y += 1;

            if (CheckShapeCollision(new Shape(currentShape, currentShape.transform.position + new Point(1, 0))) == true)
            {
                currentFallTime = finePlaceTime;
            }
        }

        public Grid GetFullGrid() { return gameGrid.Project(currentShapeProjection).Project(currentShape); }
        public Grid GetNextShapeGrid() { return nextShape; }

        public bool CheckShapeCollision(Shape shape)
        {
            if (shape.transform.position.y > gameGrid.transform.size.y - shape.transform.size.y) { return true;}

            if (shape.transform.position.x < 0) { return true; }
            if (shape.transform.position.x > gameGrid.transform.size.x - shape.transform.size.x) { return true; }

            for (int i = 0; i < shape.transform.size.y; i++)
            {
                int onGridI = i + (int)shape.transform.position.y;
                for (int j = 0; j < shape.transform.size.x; j++)
                {
                    int onGridJ = j + (int)shape.transform.position.x;
                    if (gameGrid.IsInsideGrid(onGridI, onGridJ))
                    {
                        if (gameGrid.Get(onGridI, onGridJ) == 1 && shape.Get(i, j) != 0)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public void PlaceShape(Shape shape)
        {
            for (int i = 0; i < shape.transform.size.y; i++)
            {
                int onGridI = i + (int)shape.transform.position.y;
                for (int j = 0; j < shape.transform.size.x; j++)
                {
                    int onGridJ = j + (int)shape.transform.position.x;
                    if (gameGrid.IsInsideGrid(onGridI, onGridJ))
                    {
                        if (shape.Get(i, j) == 2)
                        {
                            gameGrid.Set(onGridI, onGridJ, 1);
                        }
                    }
                    else
                    {
                        gameState = GameState.GameOver;
                    }
                }

                if (gameGrid.IsInsideGrid(onGridI, 0))
                {
                    if (CheckLine(onGridI))
                    {
                        ClearLine(onGridI);
                    }
                }
            }

            score++;
        }

        public void NextShape()
        {
            currentShape = nextShape;
            currentShape.transform.position.y = 0 - currentShape.transform.size.y;
            currentShape.transform.position.x = gameGrid.transform.size.x / 2 - currentShape.transform.size.x / 2;

            nextShape = GetRandomShape();
        }

        #region ShapeRotation
        public void RotateShapeLeft(Shape shape)
        {
            if (gameState != GameState.Running) { return; }

            Shape rotatedShape = new Shape(shape);
            rotatedShape.RotateLeft(gameGrid.transform.size.x);
            if (CheckShapeCollision(rotatedShape) == false)
            {
                shape.RotateLeft(gameGrid.transform.size.x);
            }
        }
        public void RotateShapeRight(Shape shape)
        {
            if (gameState != GameState.Running) { return; }

            Shape rotatedShape = new Shape(shape);
            rotatedShape.RotateRight(gameGrid.transform.size.x);
            if (CheckShapeCollision(rotatedShape) == false)
            {
                shape.RotateRight(gameGrid.transform.size.x);
            }
        }
        #endregion ShapeRotation

        #region ShapeMovement
        public void MoveShapeLeft(Shape shape)
        {
            if (gameState != GameState.Running) { return; }

            if (CheckShapeCollision(new Shape(shape, shape.transform.position + new Point(0, -1))) == false)
            {
                shape.transform.position.x--;
            }
        }

        public void MoveShapeRight(Shape shape)
        {
            if (gameState != GameState.Running) { return; }

            if (CheckShapeCollision(new Shape(shape, shape.transform.position + new Point(0, 1))) == false)
            {
                shape.transform.position.x++;
            }
        }

        public void DropShape(Shape shape)
        {
            if (gameState != GameState.Running) { return; }

            while (CheckShapeCollision(new Shape(shape, shape.transform.position + new Point(1, 0))) == false)
            {
                ShapeFall(shape);
            }
        }
        #endregion ShapeMovement

        #region Line
        public bool CheckLine(int line)
        {
            int sum = 0;
            for (int j = 0; j < gameGrid.transform.size.x; j++)
            {
                sum += gameGrid.Get(line, j);
            }

            if (sum == (int)gameGrid.transform.size.x) { return true; }
            return false;
        }

        public void ClearLine(int line)
        {
            for (int i = line; i > 0; i--)
            {
                for (int j = 0; j < (int)gameGrid.transform.size.x; j++)
                {
                    gameGrid.Set(i, j, gameGrid.Get(i - 1, j));
                }
            }
            for (int j = 0; j < (int)gameGrid.transform.size.x; j++) { gameGrid.Set(0, j, 0); }

            score += 10;
            if (fallTime > 0.01) { fallTime -= 0.01; }
        }
        #endregion Line

        public Shape GetRandomShape()
        {
            Random random = new Random();
            int shape = random.Next(0, 7);

            switch (shape)
            {
                case 0:
                    return new SquareShape();
                case 1:
                    return new LShape();
                case 2:
                    return new MirrorLShape();
                case 3:
                    return new ZShape();
                case 4:
                    return new MirrorZShape();
                case 5:
                    return new TShape();
                case 6:
                    return new LineShape();
                default:
                    return new SquareShape();
            }
        }
        
        public Shape ProjectShapeDown(Shape shape)
        {
            Shape projectedShape = new Shape(shape);

            for (int i = 0; i < projectedShape.transform.size.y; i++)
            {
                for (int j = 0; j < projectedShape.transform.size.x; j++)
                {
                    if (projectedShape.Get(i,j) == 2)
                    {
                        projectedShape.Set(i, j, 3);
                    }
                }
            }

            DropShape(projectedShape);
            return projectedShape;
        }

        public Shape GetCurrentShape() { return currentShape; }
        public GameState GetGameState() { return gameState; }
        public void SetGameState(GameState newGameState) { gameState = newGameState; }
        public int GetScore() { return score; }
    }
}
