using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tetris.ViewModel
{
    internal class CellViewModel : ViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }

        private Brush background;
        public Brush Background
        {
            get => background;
            set
            {
                background = value;
                OnPropertyChanged(nameof(Background));
            }
        }
    }
}
