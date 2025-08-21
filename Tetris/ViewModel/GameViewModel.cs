using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

using Tetris.Main;
using Grid = Tetris.Main.Grid;

namespace Tetris.ViewModel
{
    internal class GameViewModel : ViewModel
    {
        public GameViewModel()
        {
            game = new Game();
            game.Update(0);
            SetUpTimer();

            Cells = new ObservableCollection<CellViewModel>();
            InitializeCells();

            NextCells = new ObservableCollection<CellViewModel>();
            InitializeNextCells();
        }

        Game game;

        public ObservableCollection<CellViewModel> Cells { get; set; }
        public ObservableCollection<CellViewModel> NextCells { get; set; }

        #region ScoreText
        private string scoreText;
        public string ScoreText
        {
            get { return scoreText; }
            set
            {
                scoreText = value;
                OnPropertyChanged(nameof(ScoreText));
            }
        }
        #endregion ScoreText

        #region GameStateText
        private string gameStateText;
        public string GameStateText
        {
            get { return gameStateText; }
            set
            {
                gameStateText = value;
                OnPropertyChanged(nameof(GameStateText));
            }
        }
        #endregion GameStateText

        #region Timer
        private Stopwatch stopwatch = new Stopwatch();
        private TimeSpan lastFrameTime;

        private void SetUpTimer()
        {
            stopwatch.Start();
            lastFrameTime = stopwatch.Elapsed;
            CompositionTarget.Rendering += GameLoop;
        }

        public void StopTimer()
        {
            CompositionTarget.Rendering -= GameLoop;
            stopwatch.Stop();
        }
        #endregion Timer

        private void InitializeCells()
        {
            for (int r = 0; r < 20; r++)
            {
                for (int c = 0; c < 10; c++)
                {
                    Cells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Background = Brushes.Black
                    });
                }
            }
        }

        private void InitializeNextCells()
        {
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    NextCells.Add(new CellViewModel
                    {
                        Row = r,
                        Column = c,
                        Background = Brushes.Black
                    });
                }
            }
        }

        public void GameLoop(object sender, EventArgs e)
        {
            TimeSpan currentTime = stopwatch.Elapsed;
            double deltaTime = (currentTime - lastFrameTime).TotalSeconds;
            lastFrameTime = currentTime;

            game.Update(deltaTime);
            UpdateDisplay();
        }

        public void UpdateDisplay()
        {
            Grid gameGrid = game.GetFullGrid();
            for (int i = 0; i < gameGrid.transform.size.y; i++)
            {
                for (int j = 0; j < gameGrid.transform.size.x; j++)
                {
                    int index = i * gameGrid.transform.size.x + j;
                    switch (gameGrid.Get(i, j))
                    {
                        case 3:
                            Cells[index].Background = Brushes.HotPink;
                            break;
                        case 2:
                            Cells[index].Background = Brushes.Red;
                            break;
                        case 1:
                            Cells[index].Background = Brushes.Green;
                            break;
                        case 0:
                            Cells[index].Background = Brushes.LightGray;
                            break;
                    }
                }
            }

            Grid nextShape = game.GetNextShapeGrid();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    int index = i * 4 + j;
                    if (i < nextShape.transform.size.y && j < nextShape.transform.size.x)
                    {
                        switch (nextShape.Get(i, j))
                        {
                            case 2:
                                NextCells[index].Background = Brushes.Red;
                                break;
                            case 0:
                                NextCells[index].Background = Brushes.LightGray;
                                break;
                        }
                    }
                    else
                    {
                        NextCells[index].Background = Brushes.LightGray;
                    }
                }
            }

            ScoreText = "Score: " + game.GetScore().ToString();

            switch (game.GetGameState())
            {
                case GameState.Paused:
                    GameStateText = "Paused";
                    break;
                case GameState.GameOver:
                    GameStateText = "Game Over";
                    break;
                default:
                    GameStateText = "";
                    break;
            }
        }

        public void RotateLeft()
        {
            game.RotateShapeLeft(game.GetCurrentShape());
        }

        public void RotateRight()
        {
            game.RotateShapeRight(game.GetCurrentShape());
        }

        public void MoveLeft()
        {
            game.MoveShapeLeft(game.GetCurrentShape());
        }

        public void MoveRight()
        {
            game.MoveShapeRight(game.GetCurrentShape());
        }

        public void DropShape()
        {
            game.DropShape(game.GetCurrentShape());
        }

        public void ResetGame()
        {
            game = new Game();
        }

        public void Pause()
        {
            if (game.GetGameState() != GameState.GameOver)
            {
                if (game.GetGameState() == GameState.Running) { game.SetGameState(GameState.Paused); }
                else { game.SetGameState(GameState.Running); }
            }
        }
    }
}
