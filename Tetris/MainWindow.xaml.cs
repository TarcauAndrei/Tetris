using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Tetris.ViewModel;

namespace Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GameViewModel gameViewModel;

        public MainWindow()
        {
            InitializeComponent();

            gameViewModel = new GameViewModel();
            DataContext = gameViewModel;
            KeyDown += new KeyEventHandler(KeyBoardInput);
        }

        private void KeyBoardInput(object sender, KeyEventArgs input)
        {
            if (input.Key == Key.Q) { gameViewModel.RotateLeft(); }
            if (input.Key == Key.E) { gameViewModel.RotateRight(); }

            if (input.Key == Key.A || input.Key == Key.Left) { gameViewModel.MoveLeft(); }
            if (input.Key == Key.D || input.Key == Key.Right) { gameViewModel.MoveRight(); }

            if (input.Key == Key.S || input.Key == Key.Down) { gameViewModel.DropShape(); }

            if (input.Key == Key.P) { gameViewModel.Pause(); }

            if (input.Key == Key.R) { gameViewModel.ResetGame(); }
        }
    }
}
